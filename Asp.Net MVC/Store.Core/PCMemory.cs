using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core
{
    public class PCMemory:IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemoryId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int PCId { get; set; }
        [ForeignKey("PCId")]
        public PC @PC { get; set; }
        [ForeignKey("MemoryId")]
        public Memory @Memory { get; set; }
    }
}
