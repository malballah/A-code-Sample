
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class EmployeeCurrentProject : IDbEntity
    {
        #region Constructor
        public EmployeeCurrentProject()
        {

        }
        #endregion Constructor

        #region Properties

        [Key]
        [Required]
        [MaxLength(9)]
        public string Employee_Code { get; set; }

        [Key]
        [Required]
        [MaxLength(4)]
        public string Project_Code { get; set; }

        #endregion Properties

        #region Related Properties

        [ForeignKey("Employee_Code")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("Project_Code")]
        public virtual Project Project { get; set; }

        #endregion Related Properties

    }
}