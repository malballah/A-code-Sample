
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class WorkDate : IDbEntity
    {
        #region Constructor
        public WorkDate()
        {

        }
        #endregion Constructor

        #region Properties

        [Key]
        [Required]
        public DateTime Id_Date { get; set; }

        [Required]
        public int YYYYMM_Code { get; set; }

        [Required]
        public int Day_of_Month { get; set; }

        public Boolean Is_Non_Working_Day { get; set; }

        #endregion Properties

        #region Related Properties
        [ForeignKey("YYYYMM_Code")]
        public virtual YYYYMM YYYYMM { get; set; }

        public virtual List<TimeCard> TimeCards { get; set; }
        #endregion Related Properties

    }
}