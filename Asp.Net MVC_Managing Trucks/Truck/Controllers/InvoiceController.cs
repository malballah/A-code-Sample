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
using Truck.Models;
using Truck.ViewModels;
using Truck.Services;

namespace Truck.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class InvoiceController : BaseController
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IDbService<Invoice> _invoiceService;
        private readonly IDbService<InvoiceAttachment> _invoiceAttachmentService;
        private readonly IDbService<Attachment> _attachmentService;
        private readonly IDbService<Adjustment> _adjustmentService;
        private readonly IDbService<Customer> _customerService;
        private readonly IUnitOfWork _uow;

        public InvoiceController(IAssignmentService assignmentService,IDbService<Adjustment> adjustmentService, IDbService<Invoice> invoiceService,IDbService<InvoiceAttachment> invoiceAttachmentService, IDbService<Attachment> attachmentService, IDbService<Customer> customerService, IUnitOfWork uow)
        {
            _assignmentService = assignmentService;
            _invoiceService = invoiceService;
            _invoiceAttachmentService = invoiceAttachmentService;
            _attachmentService = attachmentService;
            _customerService = customerService;
            _adjustmentService = adjustmentService;
            _uow = uow;
        }
        
        public ActionResult Index(int? assignmentId)
        {
            if (assignmentId.HasValue)
            {
                ViewBag.AssignmentNumber = _assignmentService.Find(assignmentId.Value).Number;
                ViewBag.AssignmentId = assignmentId.Value;
            }
            ViewBag.Customers = _customerService.All.Select(item => new SelectListItem
            {
                Text = item.CustomerName,
                Value = item.Id.ToString()
            });
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetInvoices(BootGridRequest request)
        {
            var isAdmin = User.IsInRole("admin");
            if (isAdmin && !request.Params.ContainsKey("assignmentId"))
                return await SearchInvoices(request);
            int? assignmentId = null;
            if (request.Params != null && request.Params.Keys.Contains("assignmentId") && !string.IsNullOrEmpty(request.Params["assignmentId"]))
                assignmentId = int.Parse(request.Params["assignmentId"]);
            var invoicesVm = Mapper.Map<IEnumerable<InvoiceViewModel>>(_invoiceService.AllIncluding(item=>item.Assignment,item=>item.InvoiceStatus,item=>item.Assignment.AssignmentStatus).Where(item => (item.Assignment.CustomerId == CustomerId||isAdmin) && (!assignmentId.HasValue || item.AssignmentId == assignmentId.Value)));
            var response = new BootGridResponse<InvoiceViewModel>(request, invoicesVm);
            foreach (var item in response.Rows)
            {
                item.FilesCount = _invoiceAttachmentService.All.Count(ia=>ia.InvoiceId==item.Id);
            }
            return Json(response);
        }
     
        [HttpGet]
        public async Task<ActionResult> EditInvoice(int id)
        {
            var isAdmin = User.IsInRole("admin");
            var invoice = _invoiceService.AllIncluding(item=>item.Assignment,item=>item.InvoiceAttachments,item=>item.InvoiceAttachments.Select(e=>e.Attachment)).SingleOrDefault(item=>item.Id==id&& (isAdmin || item.Assignment.CustomerId == CustomerId));
            var invoiceVm = Mapper.Map<InvoiceViewModel>(invoice);
            //invoiceVm.Attachments =Mapper.Map<IEnumerable<AttachmentViewModel>>(invoice.InvoiceAttachments.Select(item=>item.Attachment));
            invoiceVm.Attachments = invoice.InvoiceAttachments.Select(item => new AttachmentViewModel
            {
                Id = item.Attachment.Id,
                Name = item.Attachment.Name.Substring(0, item.Attachment.Name.Length - 3) + "pdf",
                Path = item.Attachment.Path,//remove ~
                IsCheck=item.IsCheck
                
            }).ToList();
            if (isAdmin)
            {
                ViewBag.Statues = new List<SelectListItem>()
                {
                    new SelectListItem() {Value="3",Text = "On Hold" },
                    new SelectListItem() {Value="4",Text = "Originals Required" }
                };
                 return PartialView("AdminEditInvoice",invoiceVm);
            }
               
            return PartialView(invoiceVm);
        }

        [HttpPost]
        public async Task<JsonResult> EditInvoice(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { Error = "Complete all required fields" });
            if (model.Id.HasValue)
            {
                var isAdmin = User.IsInRole("admin");
                var invoice = _invoiceService.AllIncluding(item => item.Assignment,item=>item.Assignment.Customer,item=>item.Assignment.Invoices,item=>item.InvoiceAttachments,item=>item.InvoiceAttachments.Select(e=>e.Attachment)).SingleOrDefault(item => item.Id == model.Id.Value && (isAdmin || item.Assignment.CustomerId == CustomerId));
                if (invoice != null)
                {
                    var assignment = invoice.Assignment;
                    if (!isAdmin && assignment.StatusId != 1)
                        return Json(new { Error = "Cannot edit submitted or approved invoices" });
                    _uow.BeginTransaction();
                    invoice.Amount = model.Amount;
                    invoice.PaidAmount=invoice.Amount - (invoice.Amount * assignment.Customer.Rate/100);
                    invoice.CustomerName = model.CustomerName;
                    invoice.Number = model.Number;
                    invoice.WONumber = model.WONumber;
                    invoice.HoldReason = model.HoldReason;
                    Attachment attachment=null;
                    
                    //if check amount < paid amount and overpayment is null then create overpayment
                    decimal? adjustment = null;
                    if (isAdmin)
                    {
                        invoice.CheckAmount = model.CheckAmount;
                        invoice.CheckNumber = model.CheckNumber;
                        if (model.CheckDate != null)
                            invoice.CheckDate = model.CheckDate;
                        //upload check
                        
                        if (Request.Files.Count > 0 && Request.Files["check"] != null)
                        {
                            var ext = Request.Files["check"].FileName.Substring(Request.Files["check"].FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                            var tempFileName = UploadFileService.UploadFile(Request.Files["check"]);
                            var invoiceCheck = invoice.InvoiceAttachments.Where(item=>item.IsCheck).FirstOrDefault();
                            if (invoiceCheck?.Attachment != null)
                            { 
                                invoiceCheck.Attachment.Path = "/PdfFiles/" + tempFileName + ".pdf";
                                invoiceCheck.Attachment.OrgFilePath = "/OrgFiles/" + tempFileName + ext;
                                invoiceCheck.Attachment.Name = Request.Form["Check_Name"];
                                _attachmentService.Update(invoiceCheck.Attachment);
                            }
                            else
                            {
                                invoiceCheck = new InvoiceAttachment
                                {
                                    IsCheck = true,
                                    Attachment = new Attachment
                                    {
                                        Path = "/PdfFiles/" + tempFileName+".pdf",
                                        OrgFilePath = "/OrgFiles/" + tempFileName + ext,
                                        Name = Request.Form["Check_Name"]
                                    }
                                };
                               
                                if (invoice.InvoiceAttachments == null)
                                    invoice.InvoiceAttachments = new List<InvoiceAttachment>();
                                invoice.InvoiceAttachments.Add(invoiceCheck);
                            }
                            attachment = invoiceCheck.Attachment;
                        }
                        if (model.CheckAmount >= 0 && !string.IsNullOrEmpty(model.CheckNumber) &&
                            model.CheckDate.HasValue)
                        {
                            if (model.CheckAmount == 0)
                                invoice.StatusId = 5;
                            else
                                invoice.StatusId = 2;
                            var oldAdjustment = _adjustmentService.FindBy(item=>item.OriginInvoiceId==invoice.Id).FirstOrDefault();
                            
                                if (oldAdjustment != null)
                                {
                                    oldAdjustment.Amount = invoice.CheckAmount - invoice.Amount;
                                    if (oldAdjustment.Amount == 0)
                                        _adjustmentService.Delete(oldAdjustment);
                                    else
                                        _adjustmentService.Update(oldAdjustment);
                                }
                                else
                                {
                                    if (invoice.CheckAmount != invoice.Amount)
                                    {
                                        var newAdjustment = new Adjustment
                                        {
                                            OriginInvoice = invoice,
                                            CustomerId = assignment.CustomerId,
                                            StatusId = 1,
                                            Amount = invoice.CheckAmount - invoice.Amount
                                        };
                                        _adjustmentService.Insert(newAdjustment);
                                    }

                                }
                           
                        }
                        //implement hold invoices
                        if (model.StatusId.HasValue && (model.StatusId == 3 || model.StatusId == 4))
                        {
                            //create new assignment
                            var newAssignment = Mapper.Map<Assignment>(assignment);
                            SetAssignmnetLetter(newAssignment);
                            newAssignment.Invoices = new List<Invoice>();
                            _assignmentService.Insert(newAssignment);
                            _uow.SaveChanges();
                            invoice.AssignmentId = newAssignment.Id;
                            newAssignment.Invoices.Add(invoice);
                            assignment.Invoices.Remove(invoice);

                            _invoiceService.Update(invoice);
                            _uow.SaveChanges();
                            _assignmentService.Recaculate(newAssignment);
                            _uow.SaveChanges();
                            //send email to user
                            var user = UserManager.Users.SingleOrDefault(item=>item.Id == invoice.Assignment.UserId);
                            var msg = GetInvoiceHoldEmail(invoice, user);
                            MailService.SendMail(user.Email, "Invoice on Hold", msg);
                        }
                    }

                    _invoiceService.Update(invoice);
                    _assignmentService.Recaculate(assignment);
                    if (assignment.Invoices.Count == 0)
                        assignment.StatusId = 5;
                    _uow.SaveChanges();
                    _uow.Commit();
                    if (isAdmin)
                    {
                        adjustment = _adjustmentService.All.FirstOrDefault(item=>item.OriginInvoiceId==invoice.Id)?.Amount;
                        assignment = _assignmentService.Find(assignment.Id);
                        return Json(new { Done = 1,Adjustment = adjustment,Path= attachment?.Path, CheckId= attachment?.Id,Total=assignment.Total,TotalPayable = assignment.TotalPayable,Funded=assignment.Funded });
                    }
                    return Json(new { Done = 1, Adjustment = adjustment, CheckId = attachment?.Id});

                }

            }
            return Json(new { Error = "Invoice not exist." });
        }

        private void SetAssignmnetLetter(Assignment newAssignment)
        {
            var letters = "abcdefghijklmnopqrstuvwxyz";
            var assignmnetCount = _assignmentService.FindBy(item=>item.Number==newAssignment.Number).Count();
            newAssignment.ChildLetter = letters[assignmnetCount - 1].ToString();
        }


        [HttpPost]
        public async Task<ActionResult> InvoiceFiles(int id)
        {
            var files =
                _invoiceAttachmentService.AllIncluding(item => item.Attachment).Where(item=>item.InvoiceId==id).Select(item=>item.Attachment);
            var filesVm = Mapper.Map<IEnumerable<AttachmentViewModel>>(files);
            foreach (var item in filesVm)
            {
                item.InvoiceId = id;
            }
            return PartialView(filesVm);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var isAdmin = User.IsInRole("admin");
            var invoice = _invoiceService.All.SingleOrDefault(item => item.Id == id && (isAdmin || item.Assignment.CustomerId == CustomerId));
            if (invoice != null)
            {
                var assignment = _assignmentService.Find(invoice.AssignmentId);
                if (!isAdmin && assignment.StatusId != 1)
                    return Json(new { Error = "Cannot delete submitted, approved or paid invoices" });
                _invoiceService.Delete(id);
                _uow.SaveChanges();
                _assignmentService.Recaculate(assignment);
                _uow.SaveChanges();
                if(isAdmin)
                    {
                        return Json(new { Done = 1, Total = assignment.Total, TotalPayable = assignment.TotalPayable, Funded = assignment.Funded });
                    }
                return Json(new { Done = 1 });
            }
            return Json(new { Error = "Invoice not exist." });
        }
        [HttpPost]
        public JsonResult DeleteInvoiceFile(int invoiceId, int fileId)
        {
            var invoice = _invoiceService.AllIncluding(item => item.Assignment,item=>item.InvoiceAttachments).SingleOrDefault(item => item.Id == invoiceId && item.Assignment.UserId==UserId);
            if (invoice != null)
            {
                var isAdmin = User.IsInRole("admin");
                if (!isAdmin && invoice.Assignment.StatusId != 1)
                    return Json(new { Error = "Cannot delete submitted, approved or paid invoice files" });
                _attachmentService.Delete(fileId);
                _uow.SaveChanges();
                return Json(new {Done=1});
            }
            return Json(new {Error="File not found."});
        }
        [HttpPost]
        public async Task<JsonResult> SearchInvoices(BootGridRequest request)
        {
            var invoicesVm = await SearchInvoices(request.Params);
            var response = new BootGridResponse<InvoiceViewModel>(request, invoicesVm);
            foreach (var item in response.Rows)
            {
                item.WaitingDays = item.CheckDate.HasValue ? (item.CheckDate - item.Date).Value.Days : (DateTime.Now - item.Date).Value.Days;
            }
            return Json(response);
        }

        private async Task<IEnumerable<InvoiceViewModel>> SearchInvoices(Dictionary<string,string> parameters)
        {
            int? invoiceNumber = null;
            int? woNumber = null;
            int? assignmentNumber = null;
            int? customerId = null;
            string invoiceCustomerName = "";
            int? statusId = null;
            int? period1 = null;
            int? period2 = null;
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            if (!string.IsNullOrEmpty(parameters["invoiceNumber"]))
                invoiceNumber = int.Parse(parameters["invoiceNumber"]);
            if (!string.IsNullOrEmpty(parameters["woNumber"]))
                woNumber = int.Parse(parameters["woNumber"]);
            if (!string.IsNullOrEmpty(parameters["assignmentNumber"]))
                assignmentNumber = int.Parse(parameters["assignmentNumber"]);
            if (parameters.ContainsKey("customerId") && !string.IsNullOrEmpty(parameters["customerId"]))
                customerId = int.Parse(parameters["customerId"]);
            else if(User.IsInRole("user"))
                customerId = CustomerId;
            if (!string.IsNullOrEmpty(parameters["statusId"]))
                statusId = int.Parse(parameters["statusId"]);
            if (parameters.ContainsKey("period1")&&!string.IsNullOrEmpty(parameters["period1"]))
                period1 = int.Parse(parameters["period1"]);
            if (parameters.ContainsKey("period2") &&!string.IsNullOrEmpty(parameters["period2"]))
                period2 = int.Parse(parameters["period2"]);
            if (!string.IsNullOrEmpty(parameters["dateFrom"]))
                dateFrom = DateTime.Parse(parameters["dateFrom"]);
            if (!string.IsNullOrEmpty(parameters["dateTo"]))
                dateTo = DateTime.Parse(parameters["dateTo"]);
            if (!string.IsNullOrEmpty(parameters["invoiceCustomerName"]))
                invoiceCustomerName = parameters["invoiceCustomerName"];
            var ids = await DbContext.SearchInvoices(invoiceNumber, woNumber, assignmentNumber, customerId, invoiceCustomerName, statusId, period1,period2,dateFrom,dateTo);
            var invoices = _invoiceService.AllIncluding(item => item.Assignment, item => item.InvoiceStatus).Where(item => ids.Contains(item.Id));
            return Mapper.Map<IEnumerable<InvoiceViewModel>>(invoices);
        }

        private string GetInvoiceHoldEmail(Invoice invoice,ApplicationUser user)
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
         Invoice # "+invoice.Number+" from Assignment # "+invoice.Assignment.Number+@" was put on hold for the following reason:<br />
         <b>"+invoice.HoldReason+@"</b>
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
    }
}
