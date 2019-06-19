using HHCoApps.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HHCoApps.Repository.Dapper;
using HHCoApps.Libs;

namespace HHCoApps.Repository.Implementations
{
    internal class ProductRepository : IProductRepository
    {
        private const string SQL_CONNECTION_STRING = "HHCoApps";
        private const string PRODUCT_TABLE_NAME = "dbo.Product";

        public IEnumerable<Product> GetProductsOrderByIssuedDate()
        {
            using (var context = new HuuHanhEntities())
            {
                return GetProductsOrderByIssuedDate(context.Products).ToList();
            }
        }

        internal IQueryable<Product> GetProductsOrderByIssuedDate(IQueryable<Product> products)
        {
            return products.Where(p => !p.IsDeleted).OrderByDescending(p => p.IssuedDate).Include(p => p.Vendor).Include(p => p.Category);
        }

        public IEnumerable<Product> GetProductsByVendorUniqueIds(IEnumerable<Guid> vendorUniqueIds)
        {
            using (var context = new HuuHanhEntities())
            {
                return GetProductsByVendorUniqueIds(context.Products, vendorUniqueIds).ToList();
            }
        }

        internal IQueryable<Product> GetProductsByVendorUniqueIds(IQueryable<Product> products, IEnumerable<Guid> vendorUniqueIds)
        {
            return products.Where(p => p.IsActive && !p.IsDeleted && vendorUniqueIds.Contains(p.Vendor.UniqueId));
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var context = new HuuHanhEntities())
            {
                return GetProducts(context.Products).ToList();
            }
        }

        internal IQueryable<Product> GetProducts(IQueryable<Product> products)
        {
            return products.Where(p => !p.IsDeleted);
        }

        public void InsertProduct(Product entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var keyName = new[]
            {
                "Name"
            };
            var parameters = new
            {
                entity.BaseCost,
                entity.CategoryId,
                entity.InputCost,
                entity.IssuedDate,
                entity.IsActive,
                entity.IsDeleted,
                entity.Name,
                entity.ProductCode,
                entity.ForeignCurrency,
                entity.Stock,
                entity.VendorId
            };

            var rowAffected = DapperRepositoryUtil.InsertIfNotExist(PRODUCT_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), parameters, keyName);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void UpdateProductByUniqueId(Product entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var parameter = new
            {
                entity.BaseCost,
                entity.CategoryId,
                entity.InputCost,
                entity.IssuedDate,
                entity.IsActive,
                entity.IsDeleted,
                entity.Name,
                entity.ProductCode,
                entity.ForeignCurrency,
                entity.Stock,
                entity.VendorId
            };

            var rowAffected = DapperRepositoryUtil.UpdateRecordInTable(PRODUCT_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), "UniqueId", entity.UniqueId, parameter);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void DeleteProductsByUniqueIds(IEnumerable<Guid> productUniqueIds)
        {
            if (!productUniqueIds.Any())
                throw new ArgumentNullException(nameof(productUniqueIds));

            var parameter = new
            {
                IsActive = false,
                IsDeleted = true
            };

            var rowAffected = 0;
            foreach (var productUniqueId in productUniqueIds)
            {
                rowAffected =+ DapperRepositoryUtil.UpdateRecordInTable(PRODUCT_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), "UniqueId", productUniqueId, parameter);
            }

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void UpdateProductQuantityByProductId(int productId, int quantity)
        {
            if (productId >= 0 || quantity == 0)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);

            var keyName = new[]
            {
                "Id"
            };

            var parameters = new
            {
                Stock = quantity,
                Id = productId
            };

            var rowAffected = DapperRepositoryUtil.UpdateRecordInTable(PRODUCT_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), parameters, keyName);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }
    }
}
