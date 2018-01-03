using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "admin")]
    public class AdjustmentController : BaseController
    {
        private readonly IDbService<Assignment> _assignmentService;
        private readonly IDbService<Invoice> _invoiceService;
        private readonly IDbService<Adjustment> _adjustmentService;
        private readonly IUnitOfWork _uow;

        public AdjustmentController(IDbService<Assignment> assignmentService, IDbService<Invoice> invoiceService,
            IDbService<Adjustment> adjustmentService, IUnitOfWork uow)
        {
            _assignmentService = assignmentService;
            _invoiceService = invoiceService;
            _adjustmentService = adjustmentService;
            _uow = uow;
        }

        [HttpPost]
        public JsonResult GetAdjustments(BootGridRequest request)
        {
            int? customerId = null;
            int? assignmentId = null;
            if (request.Params != null)
            {
                if (request.Params.Keys.Contains("CustomerId") && !string.IsNullOrEmpty(request.Params["CustomerId"]))
                    customerId = int.Parse(request.Params["CustomerId"]);
                if (request.Params.Keys.Contains("AssignmentId") && !string.IsNullOrEmpty(request.Params["AssignmentId"]))
                    assignmentId = int.Parse(request.Params["AssignmentId"]);
            }
            var adjustmentsVm =
                Mapper.Map<IEnumerable<AdjustmentViewModel>>(
                    _adjustmentService.AllIncluding(item => item.OriginInvoice,item => item.OriginInvoice.Assignment)
                        .Where(item => (!customerId.HasValue || item.CustomerId == customerId)&&(item.StatusId==1||item.AppliedAssignmentId==assignmentId)));
            var response = new BootGridResponse<AdjustmentViewModel>(request, adjustmentsVm);
           
            return Json(response);
        }
        [HttpPost]
        public JsonResult ApplyAdjustment(int adjustmentId,int assignmentId)
        {
            var assignment = _assignmentService.Find(assignmentId);
            var adjustment = _adjustmentService.Find(adjustmentId);
            if (assignment == null || adjustment == null)
                return Json(new {Error="Either adjustment or assignment not found."});
            if (assignment.Funded == 0)
                return Json(new { Error = "Cannot apply adjustment, funded of current assignment = 0." });
            var newFund = assignment.Funded + adjustment.Amount;
            if (newFund >= 0)
            {
                assignment.Funded = newFund;
            }
            else
            {
                assignment.Funded = 0;
                //create new adjustment
                _adjustmentService.Insert(new Adjustment
                {
                    Amount = newFund,
                    CustomerId = assignment.CustomerId,
                    OriginInvoiceId=adjustment.OriginInvoiceId,
                    OriginAdjustmentId = adjustment.Id,
                    StatusId = 1
                });
            }
            adjustment.StatusId = 2;
            adjustment.AppliedAssignmentId = assignmentId;
            _uow.SaveChanges();
            return Json(new {Funded = assignment.Funded,Fuel = assignment.Fuel});
        }
    }
}