
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class TimeCard : IDbEntity
    {
        #region Constructor
        public TimeCard()
        {

        }
        #endregion Constructor

        #region Properties

        [Key]
        [Required]
        public int Assignment_Id { get; set; }

        [Key]
        [Required]
        public DateTime Work_Date_Id { get; set; }

        [Required]
        public float Hours_Worked { get; set; }

        #endregion Properties

        #region Related Properties

        [ForeignKey("Assignment_Id")]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey("Work_Date_Id")]
        public virtual WorkDate WorkDate { get; set; }

        #endregion Related Properties

    }
}