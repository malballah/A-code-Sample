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
using Truck.ViewModels;
using Truck.Services;

namespace Truck.Controllers.Admin
{
    [Authorize(Roles ="admin")]
    public class CustomerManagementController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbService<Customer> _customerService;
        private readonly IDbService<Assignment> _assignmentService;
        public CustomerManagementController(IUnitOfWork uow,IDbService<Customer> customerService,IDbService<Assignment> assignmentService)
        {
            _uow = uow;
            _customerService = customerService;
            _assignmentService = assignmentService;
        }
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Customers()
        {
            return PartialView();
        }
        public ActionResult Users()
        {
            return PartialView();
        }
        public JsonResult GetCustomers(BootGridRequest request)
        {
            var cusotmersVm = Mapper.Map<IEnumerable<CustomerViewModel>>(_customerService.All);
            var response = new BootGridResponse<CustomerViewModel>(request, cusotmersVm);
            foreach (var item in response.Rows)
            {
                item.NewAssignmentCount = _assignmentService.All.Count(a => a.StatusId==2 && a.CustomerId==item.Id);
            }
            return Json(response);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return PartialView(new CustomerViewModel());
            var customer = _customerService.Find(id.Value);
            return PartialView(Mapper.Map<CustomerViewModel>(customer));
        }

        [HttpPost]
        public async Task<JsonResult> Edit(CustomerViewModel model)
        {

            if (!ModelState.IsValid)
                return Json(new {Error = "Complete all required fields"});
            if (model.Id.HasValue)
            {
                var customer = _customerService.Find(model.Id.Value);
                if (customer != null)
                {
                    Mapper.Map(model, customer);
                    _customerService.Update(customer);
                    _uow.SaveChanges();
                    return Json(new { Done = 1 });
                }
                return Json(new { Error = "Customer not exist." });
            }
            else
            {
                 //create user
                var customer = Mapper.Map<Customer>(model);
                _customerService.Insert(customer);
                _uow.SaveChanges();
                return Json(new {Id = customer.Id});
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
             _customerService.Delete(id);
            _uow.SaveChanges();
             return Json(new { Done = 1 });
        }
        
    }
}
