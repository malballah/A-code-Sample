
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Core
{
    public class Attachment:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string OrgFilePath { get; set; }
        public string Name { get; set; }
        public string GoogleDriveId { get; set; }
    }
}
