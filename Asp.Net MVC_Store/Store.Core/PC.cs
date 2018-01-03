using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core
{
    public class PC:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }
        [Required]
        public int CPUId { get; set; }
        [ForeignKey("CPUId")]
        public CPU @CPU { get; set; }
        [Required]
        public int MotherboardId { get; set; }
        [ForeignKey("MotherboardId")]
        public Motherboard @Motherboard { get; set; }
        [Required]
        public int PowerSupplyId { get; set; }
        [ForeignKey("PowerSupplyId")]
        public PowerSupply @PowerSupply { get; set; }
        [Required]
        public bool AssemblyNeeded { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public virtual ICollection<PCMemory> PCMemories { get; set; }

    }
}
