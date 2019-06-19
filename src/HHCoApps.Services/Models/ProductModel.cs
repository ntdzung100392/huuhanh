using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Services.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public decimal InputCost { get; set; }
        public decimal BaseCost { get; set; }
        public string Status { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual VendorModel Vendor { get; set; }
        public virtual CategoryModel Category { get; set; }
    }
}
