
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class ProjectTracker : IDbEntity
    {
        #region Constructor
        public ProjectTracker()
        {

        }
        #endregion Constructor

        #region Properties

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(4)]
        public string Project_Code { get; set; }

        [Required]
        [MaxLength(30)]
        public string Tracker_Code { get; set; }

        #endregion Properties

        #region Related Properties

        [ForeignKey("Project_Code")]
        public virtual Project Project { get; set; }

        [ForeignKey("Tracker_Code")]
        public virtual Tracker Tracker { get; set; }

        // Part Parent of TimeCards
        public virtual List<Assignment> Assignments { get; set; }

        #endregion Related Properties

    }
}