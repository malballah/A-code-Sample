using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class YYYYMM : IDbEntity
    {
        #region Constructor
        public YYYYMM()
        {

        }
        #endregion Constructor

        #region Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [Required]
        public int Days_in_Month { get; set; }

        #endregion Properties

        #region Related Properties

        public virtual List<WorkDate> WorkDates { get; set; }
        #endregion Related Properties

    }
}