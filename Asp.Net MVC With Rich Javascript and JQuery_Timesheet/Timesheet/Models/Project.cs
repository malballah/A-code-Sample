
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class Project : IDbEntity
    {
        #region Constructor
        public Project()
        {

        }
        #endregion Constructor

        #region Properties
        [Key]
        [Required]
        [MaxLength(4)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        #endregion Properties

        #region Related Properties

        public virtual List<ProjectTracker> ProjectTrackers { get; set; }
        public virtual List<EmployeeCurrentProject> EmployeeCurrentProjects { get; set; }
        #endregion Related Properties
    }
}