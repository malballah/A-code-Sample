using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class Invoice:IEntity
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StatusId { get; set; }
        public int Number { get; set; }
        public string WONumber { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CheckAmount { get; set; }
        public string CustomerName { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CheckDate { get; set; }
        public string HoldReason { get; set; }
        public string CheckNumber { get; set; }
        [ForeignKey("AssignmentId")]
        public virtual Assignment @Assignment { get; set; }
        [ForeignKey("StatusId")]
        public virtual InvoiceStatus @InvoiceStatus { get; set; }
        public virtual ICollection<InvoiceAttachment> InvoiceAttachments{
            get;
            set;
        }
        
    }
}
