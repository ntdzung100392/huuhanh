using System;

namespace HHCoApps.Services.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string TaxCode { get; set; }
    }
}
