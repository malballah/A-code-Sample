using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.ViewModels
{
    public class MotherboardViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public int CPUSocketId { get; set; }
        [Required]
        public int MemorySlots { get; set; }
        [Required]
        public int PowerConsumption { get; set; }
        [Required]
        public decimal Price { get; set; }
        public CPUSocketViewModel @CPUSocket { get; set; }
    }
}
