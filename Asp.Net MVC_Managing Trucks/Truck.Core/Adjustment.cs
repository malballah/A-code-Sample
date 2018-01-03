using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class Adjustment:IEntity
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int OriginInvoiceId { get; set; }
        //the adjustment cause generation of this adjustment when applied to assignment
        public int OriginAdjustmentId { get; set; }
        public int? AppliedAssignmentId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
  
        [ForeignKey("OriginInvoiceId")]
        public virtual Invoice @OriginInvoice { get; set; }
        [ForeignKey("AppliedAssignmentId")]
        public virtual Assignment @AppliedAssignment { get; set; }
        [ForeignKey("StatusId")]
        public virtual AdjustmentStatus @Status { get; set; }
    }
}
