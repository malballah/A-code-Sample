using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using Truck.Core;
using Truck.Data.Infrastructure;
using Truck.Infrastructure;
using Truck.Infrastructure.Services;
using Truck.Models;
using Truck.ViewModels;
using Truck.Services;

namespace Truck.Controllers
{
    [Authorize(Roles ="admin,user")]
    public class AssignmentController : BaseController
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IDbService<Invoice> _invoiceService;
        private readonly IDbService<Customer> _customerService;
        private readonly IDbService<AssignmentStatus> _assignmentStatusService;
        private readonly IDbService<Adjustment> _overpaymentService;
        private readonly IDbService<InvoiceAttachment> _invoiceAttachmentService;
        private readonly IDbService<Attachment> _attachmentService;
        private readonly IUploadFileService _uploadFileService;
        private readonly IUnitOfWork _uow;

        public AssignmentController(IAssignmentService assignmentService, IDbService<Invoice> invoiceService, IDbService<AssignmentStatus> assignmentStatusService, IUnitOfWork uow, IDbService<Customer> customerService, IUploadFileService uploadFileService, IDbService<Adjustment> overpaymentService, IDbService<InvoiceAttachment> invoiceAttachmentService, IDbService<Attachment> attachmentService)
        {
            _assignmentService = assignmentService;
            _invoiceService = invoiceService;
            _assignmentStatusService = assignmentStatusService;
            _customerService = customerService;
            _overpaymentService = overpaymentService;
            _invoiceAttachmentService = invoiceAttachmentService;
            _attachmentService = attachmentService;
            _uploadFileService = uploadFileService;
            _uow = uow;
        }
        // GET: UserViewModels
        public ActionResult Index()
        {
            ViewBag.StatusId = Request.QueryString["StatusId"];
            ViewBag.CustomerId = Request.QueryString["CustomerId"];
            if (string.IsNullOrEmpty(Request.QueryString["StatusId"]))
            {
                var statuses = _assignmentStatusService.All;
                if (User.IsInRole("admin"))
                    statuses = statuses.Where(item=>item.Id > 2);//admin should not see new or returned assignments, he see them when they are submitted
                ViewBag.AssignmentStatuses = statuses.Select(item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Status
                });
            }
            else
                ViewBag.StatusName = _assignmentStatusService.Find(int.Parse(Request.QueryString["StatusId"]))?.Status;
            if (!string.IsNullOrEmpty(Request.QueryString["CustomerId"]))
                ViewBag.CustomerName = _customerService.Find(int.Parse(Request.QueryString["CustomerId"]))?.CustomerName;
            return View();
        }
        [HttpPost]
        public JsonResult GetAssignments(BootGridRequest request)
        {
            int? statusId = null;
            if (request.Params != null)
            {
                if (request.Params.Keys.Contains("StatusId")&& !string.IsNullOrEmpty(request.Params["StatusId"]))
                    statusId = int.Parse(request.Params["StatusId"]);
            }
            
            var isAdmin = User.IsInRole("admin");
            var assignmentsVm = Mapper.Map<IEnumerable<AssignmentViewModel>>(_assignmentService.AllIncluding(item=>item.AssignmentStatus).Where(item=>(item.CustomerId==CustomerId || isAdmin) && (!statusId.HasValue||item.StatusId==statusId.Value)&&((!isAdmin||item.StatusId>2))));
            var response = new BootGridResponse<AssignmentViewModel>(request, assignmentsVm);
            foreach (var item in response.Rows)
            {
                item.InvoicesCount = _invoiceService.All.Count(i=>i.AssignmentId==item.Id);
                item.Adjustment = _overpaymentService.FindBy(e => e.OriginInvoice.AssignmentId == item.Id && e.StatusId == 1).Sum(e => (decimal?)e.Amount);
            }
            return Json(response);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            
            var assignmentVm = new AssignmentViewModel();
            var user = UserManager.FindByName(User.Identity.Name);
                if (user != null)
                    assignmentVm.Name = user.FirstName + " " + user.LastName;
var isAdmin = User.IsInRole("admin");
            if (id.HasValue)
            {
                var assignment =
                    _assignmentService.AllIncluding(item => item.Invoices,item=>item.Invoices.Select(e=>e.InvoiceAttachments), item => item.Invoices.Select(e => e.InvoiceStatus), item=>item.Invoices.Select(e=>e.InvoiceAttachments.Select(a=>a.Attachment)))
                        .FirstOrDefault(item => item.Id == id && (isAdmin || item.CustomerId == CustomerId));
                Mapper.Map(assignment,assignmentVm);
                assignmentVm.InvoicesCount = assignment.Invoices.Count;
                if (!isAdmin)
                {
                    //load everything in assignment
                    assignmentVm.Invoices = Mapper.Map<List<InvoiceViewModel>>(assignment.Invoices);
                    foreach (var invoice in assignmentVm.Invoices)
                    {
                        invoice.Attachments =
                            assignment.Invoices.Find(item => item.Id == invoice.Id)
                                .InvoiceAttachments.Select(item => item.Attachment)
                                .Select(item => new AttachmentViewModel
                                {
                                    Id = item.Id,
                                    Name = item.Name,
                                    Path = item.Path
                                }).ToList();
                    }
                }

                ViewBag.Title = "Edit Assignment # " + assignmentVm.Number;
                if (isAdmin)
                {
                    //get previous overpaid amount
                    var adjustment =
                        _overpaymentService.FindBy(
                            item => item.CustomerId == assignment.CustomerId && item.StatusId == 1).Sum(item => (decimal?)item.Amount);
                    if (adjustment.HasValue && adjustment != 0)
                        ViewBag.Adjustment = adjustment;
                    return View("AdminEditAssignment",assignmentVm);
                }
                    
            }
            else
            {
                assignmentVm.CustomerName = _customerService.Find(CustomerId).CustomerName;
				var assignmentsForCustomer = _assignmentService.FindBy(item => item.CustomerId == CustomerId);
				assignmentVm.Number = (assignmentsForCustomer.Any()?assignmentsForCustomer.Max(item => item.Number):0) + 1;
                assignmentVm.Invoices = new List<InvoiceViewModel>() {new InvoiceViewModel {Id = 0} };
                ViewBag.Title = "Create Assignment";
            }
            if(!isAdmin)
                ViewBag.CustomerRate = _customerService.Find(CustomerId).Rate/100;
            return View(assignmentVm);
        }

        [HttpPost]
        public JsonResult Edit(AssignmentViewModel model)
        {
            var isAdmin = User.IsInRole("admin");
            ModelState.Remove(isAdmin ? "Signature" : "Funded");
            if (!ModelState.IsValid)
                return Json(new {Error = "Complete required fields."}, JsonRequestBehavior.AllowGet);

            if (model.Id.HasValue)
            {
                var assignment =
                    _assignmentService.AllIncluding(item => item.Invoices)
                        .FirstOrDefault(item => item.Id == model.Id.Value && (isAdmin || item.CustomerId == CustomerId));

                if (assignment.StatusId > 2 && !isAdmin)
                    return Json(new {Error = "Cannot edit submitted or funded assignments"});
                if (assignment.StatusId >= 4 && isAdmin)
                    return Json(new {Error = "Cannot edit closed or funded assignments"});
                if (assignment.Fuel != model.Fuel)
                {
                    assignment.Fuel = model.Fuel;
                    _assignmentService.Recaculate(assignment);
                }
                if (isAdmin)
                {
                    if (model.IsFunded)
                    {
                        assignment.StatusId = 4;
                    }
                }
                else
                {
                    assignment.Signature = model.Signature;
                    //update invoices
                    //remove deleted invoices
                    var editedInvoicesIds = model.Invoices.Where(item => item.Id.HasValue).Select(item => item.Id);
                    var deleteInvoicesIds =
                        _invoiceService.FindBy(
                            item => item.AssignmentId == model.Id && !editedInvoicesIds.Contains(item.Id))
                            .Select(item => item.Id);
                    _invoiceService.Delete(deleteInvoicesIds.ToArray());
                    var customerRate = _customerService.Find(CustomerId).Rate / 100;
                    foreach (var invoiceModel in model.Invoices)
                    {
                        var invoiceEntity =
                            _invoiceService.AllIncluding(item => item.InvoiceAttachments)
                                .SingleOrDefault(item => item.Id == invoiceModel.Id);
                        if (invoiceEntity != null)
                        {
                            invoiceEntity.Number = invoiceModel.Number;
                            invoiceEntity.WONumber = invoiceModel.WONumber;
                            invoiceEntity.CustomerName = invoiceModel.CustomerName;
                            invoiceEntity.Amount = invoiceModel.Amount;
                            invoiceEntity.PaidAmount = invoiceEntity.Amount - (invoiceEntity.Amount*customerRate/100);
                            //remove deleted attachments
                            var fileKeys = Request.Form.AllKeys.Where(item => item.Contains("OldFile"));
                            var oldFileIds = new List<int>();
                            foreach (var key in fileKeys)
                            {
                                oldFileIds.Add(int.Parse(Request.Form[key]));

                            }
                            var deletedFiles =
                                invoiceEntity.InvoiceAttachments.Where(
                                    item => !oldFileIds.Contains(item.AttachmentId)).ToList();
                            _attachmentService.Delete(deletedFiles.Select(item => item.AttachmentId).ToArray());
                            _invoiceAttachmentService.Delete(deletedFiles.Select(item => item.Id).ToArray());
                            UploadInvoiceAttachments(invoiceEntity, invoiceModel.Id.Value);
                        }
                        else
                        {
                            
                            invoiceEntity = new Invoice
                            {
                                Amount = invoiceModel.Amount,
                                AssignmentId = assignment.Id,
                                CustomerName = invoiceModel.CustomerName,
                                Date = DateTime.Now,
                                Number = invoiceModel.Number,
                                StatusId = 1,
                                WONumber = invoiceModel.WONumber,
                                PaidAmount = invoiceModel.Amount - (invoiceModel.Amount*customerRate/100)
                            };
                            assignment.Invoices.Add(invoiceEntity);
                            UploadInvoiceAttachments(invoiceEntity, invoiceModel.Id.Value);
                        }

                    }

                }
                _assignmentService.Recaculate(assignment);
                _assignmentService.Update(assignment);
                _uow.SaveChanges();
                if (isAdmin)
                {
                    return Json(new {Id = assignment.Id, Funded = assignment.Funded, StatusId = assignment.StatusId});
                }
                return Json(new {Id = assignment.Id});
            }
            else
            {
                model.Total = model.Invoices.Sum(item => item.Amount);
                model.InvoicesCount = model.Invoices.Count;
                var entity = Mapper.Map<Assignment>(model);
                entity.StatusId = 1;
                entity.UserId = UserId;
                entity.CustomerId = CustomerId;
                entity.Date = DateTime.Now;
                entity.TotalPayable = 0;
                
                var customerRate = _customerService.Find(CustomerId).Rate;
                entity.Invoices = new List<Invoice>();
                foreach (var invoiceModel in model.Invoices)
                {
                    var invEntity = new Invoice
                    {
                        Amount = invoiceModel.Amount,
                        CustomerName = invoiceModel.CustomerName,
                        Date = DateTime.Now,
                        Number = invoiceModel.Number,
                        StatusId = 1,
                        WONumber = invoiceModel.WONumber,
                        PaidAmount = invoiceModel.Amount - (invoiceModel.Amount*customerRate/100)
                    };
                    entity.Invoices.Add(invEntity);
                    entity.TotalPayable += invEntity.PaidAmount;
                    UploadInvoiceAttachments(invEntity, invoiceModel.Id.Value);
                }
                entity.Funded = entity.TotalPayable - entity.Fuel;
                entity.Fee = entity.Total*customerRate/100;
                _assignmentService.Insert(entity);
                _uow.SaveChanges();

                return Json(new {Id = entity.Id}, JsonRequestBehavior.AllowGet);
            }
        }

        private void UploadInvoiceAttachments(Invoice invoice,int id)
        {
            foreach (var item in Request.Files.AllKeys.Where(item=>item.Contains("Invoice" + id)))
            {
                var fileName = Request.Form[item];
                var ext = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                var tempFileName = _uploadFileService.UploadFile(Request.Files[item]);
                if (invoice.InvoiceAttachments == null)
                    invoice.InvoiceAttachments = new List<InvoiceAttachment>();
                invoice.InvoiceAttachments.Add(new InvoiceAttachment
                {
                    Attachment = new Attachment
                    {
                        Path = "/PdfFiles/"+ tempFileName + ".pdf",
                        OrgFilePath="/OrgFiles/"+ tempFileName + ext,
                        Name = fileName
                    }
                });
            }
        }
        [HttpPost]
        public JsonResult SubmitAssignment(int id)
        {
            var assignment = _assignmentService.FindBy(item=>item.Id==id && item.CustomerId==CustomerId).FirstOrDefault();
            if (assignment != null)
            {
                if (assignment.StatusId > 2)
                    return Json(new { Error = "Assignment allready submitted before." });
                assignment.StatusId = 3;
                assignment.SubmitDate = DateTime.Now;
                _uow.SaveChanges();
                //send submit email to admin
                SendSubmitEmail(assignment);
                return Json(new { Done = 1 });
            }
            return Json(new { Error = "Assignment not exist." });
        }

        private void SendSubmitEmail(Assignment assignment)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = UserManager.Users.Single(item => item.Id == userId);
            
            var admin = (from user in UserManager.Users
                        from role in RoleManager.Roles
                        where role.Name == "admin" && user.Roles.Select(item => item.RoleId).ToList().Contains(role.Id)
                        select user).Single();
            var msg = GetSubmitAssignmentEmailMsg(assignment,admin,currentUser);
            MailService.SendMail(admin.Email, "New submitted assignment", msg);
        }

   

        [HttpPost]
        public JsonResult ReturnAssignment(int id)
        {
            var isAdmin = User.IsInRole("admin");
            if (isAdmin)
            {
                var assignment = _assignmentService.Find(id);
                if (assignment.StatusId >= 4)
                    return Json(new { Error = "Cannot return closed or funded assignments" });
                assignment.StatusId = 2;
                assignment.ReturnReason = Request.Form["ReturnReason"];
                _uow.SaveChanges();
                var user = UserManager.Users.SingleOrDefault(item=>item.Id == assignment.UserId);
                var msg = GetAssignmentReturnEmail(assignment, user);
                MailService.SendMail(user.Email,"Assignment Returned",msg);
                return Json(new { Done = 1 });
           
            }
            return Json(new { Error = "You are not authorized to do this action" });
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var isAdmin = User.IsInRole("admin");
            var assignment = _assignmentService.FindBy(item => item.Id == id && (isAdmin || item.CustomerId == CustomerId)).FirstOrDefault();
            if (assignment != null)
            {
                if (assignment.StatusId > 2 && !isAdmin)
                    return Json(new { Error = "Cannot delete submitted or funded assignments" });
                if (assignment.StatusId >= 4 && isAdmin)
                    return Json(new { Error = "Cannot delete closed or funded assignments" });
                _assignmentService.Delete(id);
                _uow.SaveChanges();
                return Json(new {Done = 1});
            }
            return Json(new { Error = "Assignment not exist." });
        }

        private string GetSubmitAssignmentEmailMsg(Assignment assignment,ApplicationUser admin,ApplicationUser currentUser)
        {

            return @"<div>
    <center>
        <h3>Truck System</h3>
        <hr />
    </center>
    <br />
    <p>
        Hi <b>"+admin.FirstName + " " + admin.LastName + @"</b>,
    </p>
    <p>
        New assignment has been submitted by <b>" + currentUser.FirstName + " " + currentUser.LastName + @"</b>.
    </p>

    <p>Click the link bellow to open the assignment:</p>
    <p>
        <a href='" + ConfigurationManager.AppSettings["SiteAddress"]+ "/Assignment/Edit/"+assignment.Id+"'> Open assignment # " + assignment.Number + @"</a>
    </p>

    <p>
        Regards,
    </p>
    <p>
        Truck System
      </p>
      <p>
          <a href = '"+ ConfigurationManager.AppSettings["SiteAddress"] + "' >"+ ConfigurationManager.AppSettings["SiteAddress"] + @" </a>
       </p>
   </div>";
        }
        private string GetAssignmentReturnEmail(Assignment assignment, ApplicationUser user)
        {

            return @"<div>
    <center>
        <h3>Truck System</h3>
        <hr />
    </center>
    <br />
    <p>
        Hi <b>" + user.FirstName + " " + user.LastName + @"</b>,
    </p>
    <p>
         Assignment # "+assignment.Number+@" was not processed and returned back for the following reason:<br />
         <b>" + assignment.ReturnReason + @"</b>
    </p>
    <p>
        Regards,
    </p>
    <p>
        Truck System
      </p>
      <p>
          <a href = '" + ConfigurationManager.AppSettings["SiteAddress"] + "' >" + ConfigurationManager.AppSettings["SiteAddress"] + @" </a>
       </p>
   </div>";
        }
        //[Authorize(Roles ="admin")]
        //[HttpPost]
        //public JsonResult ApproveAssignment(int id)
        //{
        //    var assignment = _assignmentService.Find(id);
        //    if (assignment != null)
        //    {
        //        assignment.StatusId = 3;
        //        assignment.ApproveDate = DateTime.Now;
        //        _uow.SaveChanges();
        //        return Json(new { Done = 1 });
        //    }
        //    return Json(new { Error = "Assignment not exist." });
        //}
        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public JsonResult UnApproveAssignment(int id)
        //{
        //    var assignment = _assignmentService.Find(id);
        //    if (assignment != null)
        //    {
        //        if(assignment.StatusId != 3)
        //            return Json(new { Error = "You can unapprove approved assignments only." });
        //        assignment.StatusId = 2;
        //        assignment.ApproveDate = null;
        //        _uow.SaveChanges();
        //        return Json(new { Done = 1 });
        //    }
        //    return Json(new { Error = "Assignment not exist." });
        //}
    }
}
