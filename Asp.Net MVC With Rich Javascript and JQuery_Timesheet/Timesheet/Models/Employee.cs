
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class Employee : IDbEntity
    {
        #region Constructor
        public Employee()
        {

        }
        #endregion Constructor

        #region Properties
        [Key]
        [Required]
        [MaxLength(9)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string User_Id { get; set; }



        #endregion Properties

        #region Related Properties
        public virtual List<EmployeeCurrentProject> EmployeeCurrentProjects { get; set; }
        public virtual List<Assignment> Assignments { get; set; }
        #endregion Related Properties

    }
}