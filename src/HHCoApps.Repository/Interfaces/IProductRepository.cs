using System;
using System.Collections.Generic;
using HHCoApps.Core;

namespace HHCoApps.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProductsOrderByIssuedDate();
        IEnumerable<Product> GetProductsByVendorUniqueIds(IEnumerable<Guid> vendorUniqueIds);
        void DeleteProductsByUniqueIds(IEnumerable<Guid> productUniqueIds);
        IEnumerable<Product> GetProducts();
        void InsertProduct(Product entity);
        void UpdateProductByUniqueId(Product entity);
        void UpdateProductQuantityByProductId(int productId, int quantity);
    }
}