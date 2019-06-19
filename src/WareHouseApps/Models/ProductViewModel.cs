using System;
using System.Globalization;

namespace WareHouseApps.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public Guid CategoryUniqueId { get; set; }
        public string CategoryName { get; set; }
        public Guid VendorUniqueId { get; set; }
        public string VendorName { get; set; }
        public string Status { get; set; }
        public decimal BaseCost { get; set; }

        public string CostDisplay => string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:#,##}", InputCost);

        public decimal InputCost { get; set; }
        public string StatusDisplay => Status;
        public DateTime IssuedDate { get; set; }
        public int Warranty { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
    }
}
