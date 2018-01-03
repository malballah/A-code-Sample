using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Timesheet.Infrastructure;
using Timesheet.Models;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace Timesheet.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IDbService<Employee> _employeeService;
        private readonly IDbService<EmployeeCurrentProject> _employeeCurrentProjectService;
        private readonly IDbService<Assignment> _assignmentService;
        private readonly IDbService<ProjectTracker> _projectTrackerService;
        private readonly IDbService<Project> _projectService;
        private readonly IDbService<TimeCard> _timeCardService;
        private readonly IDbService<WorkDate> _workDateService;
        private readonly IDbService<YYYYMMLocked> _yyyymmLockedService;
        private readonly IUnitOfWork _uow;

        public string EmployeeName => string.Join("@",
            User.Identity.Name.Split(new string[] {"\\"}, StringSplitOptions.RemoveEmptyEntries).Reverse());
        public string EmployeeCode => _employeeService.All.SingleOrDefault(item=>item.User_Id.ToLower()==EmployeeName.ToLower())?.Code;

        public TimesheetController(IDbService<YYYYMMLocked> yyyymmLockedService, IDbService<WorkDate> workDateService,IDbService<TimeCard> timeCardService, IDbService<Project> projectService,
            IDbService<ProjectTracker> projectTrackerService, IDbService<Assignment> assignmentService,
            IDbService<Employee> employeeService, IDbService<EmployeeCurrentProject> employeeCurrentProjectService,
            IUnitOfWork uow)
        {
            _employeeService = employeeService;
            _employeeCurrentProjectService = employeeCurrentProjectService;
            _assignmentService = assignmentService;
            _projectTrackerService = projectTrackerService;
            _projectService = projectService;
            _timeCardService = timeCardService;
            _workDateService = workDateService;
            _yyyymmLockedService = yyyymmLockedService;
            _uow = uow;
        }

        public ActionResult Index()
        {
            var maxDate = _workDateService.All.Max(item=>item.Id_Date);
            ViewBag.Years = _workDateService.All.Select(item=>item.Id_Date.Year).Distinct().ToList().Select(item=>new SelectListItem()
            {
                Text=item.ToString(),
                Value=item.ToString(),
                Selected=item==maxDate.Year
            });
            ViewBag.Months = _workDateService.All.Select(item=>item.Id_Date.Month).Distinct().ToList().Select(item => new SelectListItem()
            {
                Text =new DateTime(DateTime.Now.Year,item,1).ToString("MMMM"),
                Value = item.ToString(),
                Selected = item == maxDate.Month
            });
            
            return View(GetTimsheet(maxDate.Year, maxDate.Month));
        }

        public ActionResult Timesheet(int year,int month)
        {
            return PartialView(GetTimsheet(year,month));
        }
        [HttpGet]
        public ActionResult SelectProjects()
        {
            var currentProjects = _employeeCurrentProjectService.FindBy(
                item => item.Project_Code != "NCHG" && item.Employee_Code == EmployeeCode)
                .Select(item => item.Project)
                .Distinct()
                .OrderBy(item => item.Code).ToList()
                .Select(item => new ProjectViewModel(item)).ToList();

            var model = new SelectProjectEditModel
            {
                SelectedProjects = currentProjects,
                ProjectList = (from project in _projectService.FindBy(item => item.Code != "NCHG").Distinct()
                .OrderBy(item=>item.Code).ToList().Select(item=>new ProjectViewModel(item))
                where currentProjects.All(item=>item.Code!=project.Code)
                select project).Distinct().ToList()
            };
            foreach (var item in model.SelectedProjects)
            {
                item.CanRemove =
                    !_timeCardService.All.Any(
                        e =>
                            e.Assignment.Employee_Code == EmployeeCode &&
                            e.Assignment.ProjectTracker.Project_Code == item.Code && e.Hours_Worked > 0);
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult SelectProjects(List<int> projects)
        {
            
        }
        [HttpPost]
        public JsonResult Save(TimesheetSaveModel model)
        {
            if (model.WorkedHours != null&&model.WorkedHours.Count > 0)
            {
                var yyyymmCode = int.Parse(model.WorkedHours.First().WorkDateId.Year + "" + model.WorkedHours.First().WorkDateId.Month.ToString().PadLeft(2, '0'));
                var locked = _yyyymmLockedService.All.Any(item => item.Code >= yyyymmCode);
                if (locked)
                    return Json(new {Error="This month is locked, you cannot edit in it"},JsonRequestBehavior.AllowGet);
                _uow.BeginTransaction(IsolationLevel.Unspecified);
                foreach (var item in model.WorkedHours)
                {
                    if (!item.AssignmentId.HasValue)
                    {
                        //check if assignment exist with employee code and project_tracker
                        var assignment =_assignmentService.All.SingleOrDefault(e=>e.Project_Tracker_Id==item.ProjectTrackerId&&e.Employee_Code==EmployeeCode);
                        if (assignment == null)
                        {
                            assignment = new Assignment
                            {
                                Employee_Code = EmployeeCode,
                                Project_Tracker_Id = item.ProjectTrackerId,
                            };
                            _assignmentService.Insert(assignment);
                            _uow.SaveChanges();
                        }
                        item.AssignmentId = assignment.Id;
                    }
                    var timeCard=_timeCardService.All.SingleOrDefault(e=>e.Assignment_Id==item.AssignmentId&&e.Work_Date_Id==item.WorkDateId);
                    if (timeCard != null)
                    {
                        timeCard.Hours_Worked = item.Hours;
                        _timeCardService.Update(timeCard);
                    }
                    else
                    {
                        _timeCardService.Insert(new TimeCard
                        {
                            Assignment_Id=item.AssignmentId.Value,
                            Work_Date_Id=item.WorkDateId,
                            Hours_Worked=item.Hours
                        });
                    }
                }
                _uow.SaveChanges();
                _uow.Commit();
                return Json(new {Done = 1 },JsonRequestBehavior.AllowGet);
            }
            return Json(new { Done = 0 }, JsonRequestBehavior.AllowGet);
        }
        
        private TimesheetEditModel GetTimsheet(int year,int month)
        {
            int yearmonthCode = int.Parse(year + "" + month.ToString().PadLeft(2, '0'));
            var timesheet = new TimesheetEditModel
            {
                Date=new DateTime(year,month,1),
                ProjectTrakers = (from p in _projectTrackerService.All.Include(item=>item.Project).Include(item=>item.Tracker)
                                  from e in _employeeCurrentProjectService.All
                                  where p.Project_Code == e.Project_Code && e.Employee_Code == EmployeeCode
                                  select p).ToList(),
                WorkDates=_workDateService.FindBy(item=>item.YYYYMM_Code == yearmonthCode).ToList(),
                TimeCards = (from e in _employeeCurrentProjectService.All
                             from a in _assignmentService.All
                             from t in _timeCardService.All
                             where a.Employee_Code==e.Employee_Code && t.Assignment_Id==a.Id && e.Employee.Code == EmployeeCode
                             && a.ProjectTracker.Project_Code==e.Project_Code && t.WorkDate.YYYYMM_Code==yearmonthCode
                             select t).ToList(),
                Locked = _yyyymmLockedService.All.Any(item=>item.Code>=yearmonthCode)
            };
            //if(timesheet.WorkDates.Count==0)
            //    for (var i = 1; i <= DateTime.DaysInMonth(year,month); i++)
            //    {
            //        timesheet.WorkDates.Add(new Models.WorkDate {
            //            Day_of_Month = i,
            //            Id_Date=new DateTime(year,month,i),
            //            YYYYMM_Code= yearmonthCode
            //        });
            //    }
          
            //var context = new PrincipalContext(ContextType.Domain, "ldap://localhost:63083/dc=maxcrc,dc=com");
            //var user = UserPrincipal.FindByIdentity(context, User.Identity.Name);
            //WindowsIdentity clientId = (WindowsIdentity)User.Identity;
            //using (var context = new PrincipalContext(ContextType.Domain, "DCESTAGIO"))
            //{
            //    var principal = UserPrincipal.FindByIdentity(context, User.Identity.Name);
            //    var firstName = principal.GivenName;
            //    var lastName = principal.Surname;
            //}
            return timesheet;
        }
    }
}