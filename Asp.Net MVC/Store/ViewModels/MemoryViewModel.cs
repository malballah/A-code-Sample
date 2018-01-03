using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.ViewModels
{
    public class MemoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public int PowerConsumption { get; set; }
        [Required]
        public decimal Price { get; set; }
       
    }
}
