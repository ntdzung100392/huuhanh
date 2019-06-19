using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HHCoApps.Core;

namespace HHCoApps.Repository
{
    public interface IVendorRepository
    {
        IEnumerable<Vendor> GetVendors();
        void InsertVendor(Vendor entity);
        void UpdateVendor(Vendor entity);
        void DeleteVendorByUniqueId(Guid vendorUniqueId);
    }
}
