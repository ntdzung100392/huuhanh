using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Services.Models
{
    public class VendorModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int ContactId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ContactModel Contact { get; set; }
    }
}
