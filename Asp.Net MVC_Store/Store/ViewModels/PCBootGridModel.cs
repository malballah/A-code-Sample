using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Store.Web.ViewModels
{
    public class PCBootGridModel
    {
        public int Id { get; set; }
       public string CustomerName { get; set; }
       public string CPU { get; set; }
      public string Motherboard { get; set; }
        public int MemoryAmount { get; set; }
     public string PowerSupply { get; set; }
      public bool AssemblyNeeded { get; set; }
      public decimal TotalPrice { get; set; }

    }
}
