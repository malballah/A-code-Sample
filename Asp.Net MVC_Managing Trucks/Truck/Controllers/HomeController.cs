using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Truck.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
                return RedirectToAction("Index", "Assignment", new { StatusId = 3 });
            if (User.IsInRole("user"))
                return RedirectToAction("Index", "Assignment", new { StatusId = 4 });
            return View();
        }
    }
}