using HHCoApps.Services.Models;
using System;
using System.Collections.Generic;

namespace HHCoApps.Services.Interfaces
{
    public interface IProductServices
    {
        IEnumerable<Guid> GetProductUniqueIdsByVendorUniqueIds(IEnumerable<Guid> vendorUniqueIds);
        IEnumerable<ProductModel> GetProductsOrderByIssuedDate();
        void InsertProduct(ProductModel model);
        void UpdateProductByUniqueId(ProductModel model);
        IEnumerable<ProductModel> GetAllProduct();
        void DeleteProductsByUniqueIds(IEnumerable<Guid> productUniqueIds);
    }
}
