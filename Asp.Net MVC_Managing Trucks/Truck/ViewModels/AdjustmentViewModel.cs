using System.ComponentModel.DataAnnotations;

namespace Truck.ViewModels
{
    public class AdjustmentViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int AppliedAssignmentId { get; set; }
        public int AssignmentNumber
        {
            get;
            set;
        }
        public int OriginInvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}
