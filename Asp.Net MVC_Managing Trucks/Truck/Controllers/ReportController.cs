using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Truck.Core;
using Truck.Data.Infrastructure;
using Truck.Infrastructure;
using Truck.Services;
using Truck.ViewModels;

namespace Truck.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class ReportController : BaseController
    {
        private readonly IDbService<Assignment> _assignmentService;
        private readonly IDbService<Invoice> _invoiceService;
        private readonly IDbService<InvoiceStatus> _invoiceStatusService;
        private readonly IDbService<Customer> _customerService;
        private readonly IDbService<AssignmentStatus> _assignmentStatusService;
        private readonly IDbService<InvoiceAttachment> _invoiceAttachmentService;
        public ReportController(IDbService<Assignment> assignmentService, IDbService<Invoice> invoiceService, IDbService<AssignmentStatus> assignmentStatusService, IDbService<Customer> customerService, IDbService<InvoiceAttachment> invoiceAttachmentService,IDbService<InvoiceStatus> invoiceStatusService)
        {
            _assignmentService = assignmentService;
            _invoiceService = invoiceService;
            _assignmentStatusService = assignmentStatusService;
            _customerService = customerService;
            _invoiceAttachmentService = invoiceAttachmentService;
            _invoiceStatusService= invoiceStatusService;
        }

        public ActionResult PaidUnpaidInvoices(string status)
        {
            ViewBag.StatusId = status == "paid" ? 2 : 1;
            ViewBag.Customers =_customerService.All.Select(item=>new SelectListItem {Value=item.Id.ToString(),Text=item.CustomerName});
            ViewBag.InvoiceStatuses = _invoiceStatusService.All.Select(item=>new SelectListItem {Value=item.Id.ToString(),Text=item.Status});;
            return View();
        }
    }
}