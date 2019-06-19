using HHCoApps.Services.Models;
using System.Collections.Generic;

namespace HHCoApps.Services.Interfaces
{
    public interface IVendorServices
    {
        IEnumerable<VendorModel> GetVendors();
        void InsertVendor(VendorModel model);
        void UpdateVendor(VendorModel model);
        void DeleteVendorWithProducts(VendorModel model);
    }
}
