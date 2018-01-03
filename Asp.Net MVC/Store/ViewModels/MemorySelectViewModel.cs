using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Store.Web.ViewModels
{
    public class MemorySelectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PowerConsumption { get; set; }
        public Decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsSelected { get; set; }
    }
}
