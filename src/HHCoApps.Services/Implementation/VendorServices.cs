using AutoMapper;
using HHCoApps.Core;
using HHCoApps.Repository;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace HHCoApps.Services.Implementation
{
    internal class VendorServices : IVendorServices
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IContactServices _contactServices;
        private readonly IProductServices _productServices;

        public VendorServices(IVendorRepository vendorRepository, IContactServices contactServices, IProductServices productServices)
        {
            _vendorRepository = vendorRepository;
            _contactServices = contactServices;
            _productServices = productServices;
        }

        public IEnumerable<VendorModel> GetVendors()
        {
            var vendors = _vendorRepository.GetVendors();
            return vendors.Any() ? Mapper.Map<IEnumerable<VendorModel>>(vendors) : Enumerable.Empty<VendorModel>();
        }

        public void InsertVendor(VendorModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(1)))
            {
                var contact = _contactServices.InsertContact(model.Contact);
                var contactId = _contactServices.GetContactByUniqueIds(new[] { contact.UniqueId }).First().Id;
                model.ContactId = contactId;

                var entity = Mapper.Map<Vendor>(model);
                _vendorRepository.InsertVendor(entity);
                transactionScope.Complete();
            }
        }

        public void UpdateVendorByUniqueId(VendorModel model)
        {
            var lastContactUniqueId = _contactServices.GetContactUniqueIdByContactId(model.ContactId);
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(1)))
            {

            }
        }

        public void UpdateVendor(VendorModel model)
        {
            var entity = Mapper.Map<Vendor>(model);
            _vendorRepository.UpdateVendor(entity);
        }

        public void DeleteVendorWithProducts(VendorModel model)
        {
            var entity = Mapper.Map<Vendor>(model);
            var productUniqueIds = _productServices.GetProductUniqueIdsByVendorUniqueIds(new[] { entity.UniqueId });

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(1)))
            {
                if (productUniqueIds.Any())
                {
                    _productServices.DeleteProductsByUniqueIds(productUniqueIds);
                }
                _vendorRepository.DeleteVendorByUniqueId(entity.UniqueId);
                transaction.Complete();
            }
        }
    }
}
