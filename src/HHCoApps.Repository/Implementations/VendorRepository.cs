using HHCoApps.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HHCoApps.Libs;
using HHCoApps.Repository.Dapper;

namespace HHCoApps.Repository.Implementations
{
    internal class VendorRepository : IVendorRepository
    {
        private const string SQL_CONNECTION_STRING = "HHCoApps";
        private const string VENDOR_TABLE_NAME = "dbo.Vendor";

        public VendorRepository()
        {
        }

        public IEnumerable<Vendor> GetVendors()
        {
            using (var context = new HuuHanhEntities())
            {
                return GetVendors(context.Vendors).ToList();
            }
        }

        public void InsertVendor(Vendor entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity.Name));

            var keyName = new[]
            {
                "Name"
            };
            var parameters = new
            {
                entity.Name,
                entity.Note
            };

            var rowAffected = DapperRepositoryUtil.InsertIfNotExist(VENDOR_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), parameters, keyName);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void DeleteVendorByUniqueId(Guid vendorUniqueId)
        {
            if(vendorUniqueId == null || vendorUniqueId == Guid.Empty)
                throw new ArgumentNullException(nameof(vendorUniqueId));

            var parameters = new
            {
                IsActive = false,
                IsDeleted = true
            };

            var rowAffected = DapperRepositoryUtil.UpdateRecordInTable(VENDOR_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), "UniqueId", vendorUniqueId, parameters);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void UpdateVendor(Vendor entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity.Name));

            var parameters = new
            {
                entity.Name,
                entity.Note,
                entity.IsActive,
                entity.IsDeleted
            };

            var rowAffected = DapperRepositoryUtil.UpdateRecordInTable(VENDOR_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), "UniqueId", entity.UniqueId, parameters);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        internal Vendor GetVendorByUniqueId(Guid vendorUniqueId, IQueryable<Vendor> vendors)
        {
            return vendors.FirstOrDefault(v => v.UniqueId == vendorUniqueId);
        }

        internal IQueryable<Vendor> GetVendors(IQueryable<Vendor> vendors)
        {
            return vendors.Where(v => !v.IsDeleted).Include(v => v.Contacts).OrderBy(s => s.Name);
        }

        internal Vendor GetVendorByName(string vendorName, IQueryable<Vendor> vendors)
        {
            return vendors.FirstOrDefault(v => v.Name.Equals(vendorName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
