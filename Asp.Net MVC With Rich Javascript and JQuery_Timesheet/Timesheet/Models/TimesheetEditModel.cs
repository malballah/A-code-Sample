using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Models
{
    public class TimesheetEditModel
    {
        public TimesheetEditModel()
        {
            
        }
        public string EmployeeCode { get; set; }
        public bool Locked { get; set; }
        public DateTime Date { get; set; }
        public List<ProjectTracker> ProjectTrakers { get; set; }
        public List<WorkDate> WorkDates { get; set; }
        public List<TimeCard> TimeCards { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}