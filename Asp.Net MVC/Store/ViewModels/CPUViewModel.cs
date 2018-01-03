using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Core;

namespace Store.Web.ViewModels
{
    public class CPUViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public int CPUSocketId { get; set; }
        [Required]
        public int PowerConsumption { get; set; }
        [Required]
        public decimal Price { get; set; }
        public CPUSocketViewModel @CPUSocket { get; set; }
    }
}
