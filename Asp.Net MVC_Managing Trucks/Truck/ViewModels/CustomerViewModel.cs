using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Truck.ViewModels
{
    public class CustomerViewModel
    {
        public int? Id { get; set; }
        [Display(Name ="Company Name")]
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "Client #")]
        [Required]
        public string ClientNumber { get; set; }
        [Display(Name = "Owner Name")]
        [Required]
        public string OwnerName { get; set; }
        [Display(Name = "MC #")]
        [Required]
        public string MCNumber { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "Phone")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name ="Rate(%)")]
        [Required]
        public decimal Rate { get; set; }

        public int NewAssignmentCount { get; set; }
    }
}