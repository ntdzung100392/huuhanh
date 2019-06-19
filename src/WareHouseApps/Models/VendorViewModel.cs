using System;

namespace WareHouseApps.Models
{
    public class VendorViewModel
    {
        public Guid UniqueId { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}
