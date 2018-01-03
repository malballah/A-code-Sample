
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class InvoiceAttachment:IEntity
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int AttachmentId { get; set; }
        public bool IsCheck { get; set; }
        public Attachment @Attachment { get; set; }
    }
}
