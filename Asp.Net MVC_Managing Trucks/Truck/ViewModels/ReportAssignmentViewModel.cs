using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Truck.ViewModels
{
    public class ReportAssignmentViewModel
    {
        public int? Id { get; set; }
        public int Number { get; set; }
        [Required]
        public decimal Fuel { get; set; }
        [Required]
        public decimal Funded { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Signature")]
        public string Signature { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        [Display(Name="Invoice Count")]
        public int InvoicesCount { get; set; }
        public string Name { get; set; }
        public ICollection<InvoiceViewModel> Invoices { get; set; }
        public int WaitingDays { get; set; }
    }
}