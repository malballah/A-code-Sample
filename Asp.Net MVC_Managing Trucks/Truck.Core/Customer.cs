using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class Customer:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ClientNumber { get; set; }
        public string OwnerName { get; set; }
        public string MCNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public decimal Rate { get; set; } 
    }
}
