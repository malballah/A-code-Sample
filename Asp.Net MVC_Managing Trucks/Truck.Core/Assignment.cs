using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class Assignment:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Number { get; set; }
        public string ChildLetter { get; set; }
        public decimal Fuel { get; set; }
        public decimal Funded { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal Fee { get; set; }
        public string Signature { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ReturnReason { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer @Customer { get; set; }
        [ForeignKey("StatusId")]
        public virtual AssignmentStatus @AssignmentStatus { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}
