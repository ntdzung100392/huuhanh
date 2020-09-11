using HHCoApps.Repository;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.Core;

namespace HHCoApps.Services.Implementation
{
    internal class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public ProductServices(IProductRepository productRepository, ICategoryServices categoryServices, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        public IEnumerable<Guid> GetProductUniqueIdsByVendorUniqueIds(IEnumerable<Guid> vendorUniqueIds)
        {
            var products = _productRepository.GetProductsByVendorUniqueIds(vendorUniqueIds);
            return products.Select(p => p.UniqueId);
        }

        public IEnumerable<ProductModel> GetProductsOrderByIssuedDate()
        {
            var products = _productRepository.GetProductsOrderByIssuedDate();
            return products.Any() ? products.Select(_mapper.Map<ProductModel>).ToList() : Enumerable.Empty<ProductModel>();
        }

        public void InsertProduct(ProductModel model)
        {
            var entity = _mapper.Map<Product>(model);
            entity.ProductCode = GenerateProductCode();
            _productRepository.InsertProduct(entity);
        }

        public void UpdateProductByUniqueId(ProductModel model)
        {
            var entity = _mapper.Map<Product>(model);
            _productRepository.UpdateProductByUniqueId(entity);
        }

        private string GenerateProductCode()
        {
            var products = _productRepository.GetProducts();
            if (!products.Any())
                return "SP1";

            var productsCount = products.Count();
            return $"SP{productsCount}";
        }

        public IEnumerable<ProductModel> GetAllProduct()
        {
            return _productRepository.GetProducts().Select(_mapper.Map<ProductModel>).ToList();
        }

        public void DeleteProductsByUniqueIds(IEnumerable<Guid> productUniqueIds)
        {
            _productRepository.DeleteProductsByUniqueIds(productUniqueIds);
        }

        public void UpdateProductStock(int productId, int quantity)
        {

        }
    }
}
