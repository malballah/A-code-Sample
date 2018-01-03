using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Models
{
    public class SelectProjectEditModel
    {
       public List<ProjectViewModel> ProjectList { get; set; }
        public List<ProjectViewModel> SelectedProjects { get; set; }
    }
}