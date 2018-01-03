using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Truck.ViewModels
{
    public class AssignmentViewModel
    {
        public int? Id { get; set; }
        public int Number { get; set; }
        public string ChildLetter { get; set; }
        [Required]
        public decimal Fuel { get; set; }
        public decimal Funded { get; set; }
        public decimal Total { get; set; }
        [Display(Name="Total Payable")]
        public decimal TotalPayable { get; set; }
        public decimal Fee { get; set; }
        public decimal? Adjustment { get; set; }
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
        public IList<InvoiceViewModel> Invoices { get; set; }
        [Display(Name="Is Funded")]
        public bool IsFunded { get; set; }
        public string ReturnReason { get; set; }
    }
}