using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Truck.Core;

namespace Truck.ViewModels
{
    public class InvoiceViewModel
    {
        public int? Id { get; set; }
        public int AssignmentId { get; set; }
        public int AssignmentStatusId { get; set; }
        public int? StatusId { get; set; }
        public string Status { get; set; }
        public int AssignmentNumber { get; set; }
        [Required]
        [Display(Name ="Invoice #")]
        public int Number { get; set; }
        [Required]
        [Display(Name = "Work Order #")]
        public string WONumber { get; set; }
        [Required]
        [Display(Name = "Invoice Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Adjustment")]
        public decimal AdjustmentAmount { get; set; }
        [Display(Name = "Amount Payable")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Check Amount")]
        public decimal CheckAmount { get; set; }      
        [Required]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        public DateTime? Date { get; set; }
        [Display(Name ="Check Receive Date")]
        [DisplayFormat(DataFormatString="dd/MM/yyyy")]
        public DateTime? CheckDate { get; set; }
        [Display(Name ="Hold Reason")]
        public string HoldReason { get; set; }
        [Display(Name ="Check Number")]
        public string CheckNumber { get; set; }
        public int FilesCount { get; internal set; }
        public IList<AttachmentViewModel> Attachments { get; set; }
        public AdjustmentViewModel Adjustment { get; set; }
        public int WaitingDays { get; set; }
    }
}
