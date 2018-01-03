using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Models
{
    public class TimesheetSaveModel
    {
       public List<WorkItem> WorkedHours { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int YYYYMM => int.Parse(Year + Month.ToString().PadLeft(2, '0'));
    }

    public class WorkItem
    {
        public int ProjectTrackerId { get; set; }
        public int? AssignmentId { get; set; }
        public DateTime WorkDateId { get; set; }
        public float Hours { get; set; }
    }
}