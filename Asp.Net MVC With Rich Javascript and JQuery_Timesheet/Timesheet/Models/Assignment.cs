
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class Assignment: IDbEntity
    {
        #region Constructor
        public Assignment()
        {

        }
        #endregion Constructor

        #region Properties

        [Key]
        [Required]
        public int Id { get; set; }


        [Required]
        [MaxLength(9)]
        public string Employee_Code { get; set; }

        [Required]
        public int Project_Tracker_Id { get; set; }


        #endregion Properties

        #region Related Properties

        [ForeignKey("Employee_Code")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("Project_Tracker_Id")]
        public virtual ProjectTracker ProjectTracker { get; set; }

        public virtual List<TimeCard> TimeCards { get; set; }

        #endregion Related Properties

    }
}