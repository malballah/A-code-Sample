namespace Truck.ViewModels
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public bool IsCheck { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
