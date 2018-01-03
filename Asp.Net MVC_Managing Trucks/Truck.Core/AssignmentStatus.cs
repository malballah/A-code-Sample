using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class AssignmentStatus:IEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
