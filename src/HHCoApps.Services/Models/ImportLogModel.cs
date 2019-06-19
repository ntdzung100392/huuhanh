using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Services.Models
{
    public class ImportLogModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public System.DateTime Created { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalBaseCost { get; set; }
        public string ForeignCurrency { get; set; }

        public virtual ProductModel Product { get; set; }
        public virtual UserModel User { get; set; }
        public virtual VendorModel Vendor { get; set; }
    }
}
