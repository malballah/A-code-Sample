using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class YYYYMMLocked : IDbEntity
    {
        #region Constructor
        public YYYYMMLocked()
        {

        }
        #endregion Constructor

        #region Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }


        #endregion Properties

        #region Related Properties

        #endregion Related Properties

    }
}