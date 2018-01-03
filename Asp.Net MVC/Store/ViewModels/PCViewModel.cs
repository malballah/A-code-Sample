using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Store.Web.ViewModels
{
    public class PCViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [Required]
        [DisplayName("CPU")]
        public int CPUId { get; set; }
        public CPUViewModel @CPU { get; set; }
        [Required]
        [DisplayName("Motherboard")]
        public int MotherboardId { get; set; }
        public MotherboardViewModel @Motherboard { get; set; }
        [Required]
        [DisplayName("PowerSupply")]
        public int PowerSupplyId { get; set; }
        public PowerSupplyViewModel @PowerSupply { get; set; }
        [Required]
        [DisplayName("Assembly Needed")]
        public bool AssemblyNeeded { get; set; }
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        public virtual ICollection<PCMemoryViewModel> PCMemories { get; set; }
        public  IEnumerable<MemorySelectViewModel> SelectedMemories { get; set; }
    }
}
