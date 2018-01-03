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
using Truck.ViewModels;

namespace Truck.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class UserManagementController : BaseController
    {
        private readonly IDbService<Customer> _customerService;

        public UserManagementController(IDbService<Customer> customerService)
        {
            _customerService = customerService;
        }

        // GET: UserViewModels
        public ActionResult Index()
        {
            return PartialView();
        }

        public JsonResult GetUsers(BootGridRequest request)
        {
            var usersVm = Mapper.Map<IEnumerable<UserViewModel>>(UserManager.Users.Include(item => item.Customer));
            return Json(new BootGridResponse<UserViewModel>(request, usersVm));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.Customers = _customerService.All.Select(item => new SelectListItem
            {
                Text = item.CustomerName,
                Value = item.Id.ToString()
            });
            if (string.IsNullOrEmpty(id))
                return PartialView(new UserViewModel());
            var user = await UserManager.FindByIdAsync(id);
            return PartialView(Mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<JsonResult> Edit(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new {Error = "Complete all required fields"});
                if (string.IsNullOrEmpty(model.Id))
                {
                    //create user
                    var user = Mapper.Map<ApplicationUser>(model);
                    user.Id = Guid.NewGuid().ToString();
                    var randomPassword = GeneratePassword(8);
                    var result = await UserManager.CreateAsync(user, randomPassword);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, "user");
                        //send email
                        var currentUser = UserManager.Users.SingleOrDefault(item => item.Id == UserId);

                        string msg = GetRegisterUsertEmailMsg();
                        msg = msg.Replace("@username", model.FirstName);
                        msg = msg.Replace("@byusername", currentUser?.FirstName + " " + currentUser?.LastName);
                        msg = msg.Replace("@email", model.Email);
                        msg = msg.Replace("@password", randomPassword);
                        msg = msg.Replace("@loginlink",
                            ConfigurationManager.AppSettings["SiteAddress"] + "/Account/Login");
                        msg = msg.Replace("@siteaddress", ConfigurationManager.AppSettings["SiteAddress"]);
                        MailService.SendMail(model.Email, "Truck System User Details", msg);
                        return Json(new {Done = 1});
                    }
                    return Json(new {Error = "Cannot create user, please try again later."});
                }
                else
                {
                    var user = await IdentityDb.Users.SingleOrDefaultAsync(item => item.Id == model.Id);
                    if (user != null)
                    {

                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.CustomerId = model.CustomerId;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        IdentityDb.SaveChanges();
                        return Json(new {Done = 1});
                    }
                    return Json(new {Error = "User not exist."});
                }
            }
            catch (Exception exp)
            {
                return null;
            }

        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            var result = await UserManager.DeleteAsync(await UserManager.FindByIdAsync(id));
            if (result.Succeeded)
                return Json(new {Done = 1});
            return Json(new {Error = "Cannot delete this user."});
        }

        private string GetRegisterUsertEmailMsg()
        {
            return @"<div>
    <center>
        <h3>Truck System</h3>
        <hr />
    </center>
    <br />
    <p>
        Hi <b>@username</b>,
    </p>
    <p>
        You have been added to the Truck System by <b>@byusername</b>.
    </p>
    <p>
        Your username is <b>@email</b>
    </p>
    <p>
        Your password is <b>@password</b>
    </p>
    <p>Click the bellow link to login</p>
    <p>
        <a href='@loginlink'>@loginlink</a>
    </p>

    <p>
        Regards,
    </p>
    <p>
        Truck System
      </p>
      <p>
          <a href = '@siteaddress' > @siteaddress </a>
       </p>
   </div>";
        }
    }
}
