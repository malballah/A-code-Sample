using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Core;


namespace Store.Web.ViewModels
{
    public class PCMemoryViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemoryId { get; set; }
        public int Count { get; set; }
        [Required]
        public int PCId { get; set; }
        public Memory @Memory { get; set; }
    }
}
