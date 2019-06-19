using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.Core;
using HHCoApps.Libs;
using HHCoApps.Repository.Dapper;
using HHCoApps.Repository.Interfaces;

namespace HHCoApps.Repository.Implementations
{
    internal class ContactRepository : IContactRepository
    {
        private const string SQL_CONNECTION_STRING = "HHCoApps";
        private const string CONTACT_TABLE_NAME = "dbo.Contact";

        public void InsertContact(Contact entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var parameters = new[]
            {
                new
                {
                    entity.UniqueId,
                    entity.TaxCode,
                    entity.Address,
                    entity.Email,
                    entity.Fax,
                    entity.IsActive,
                    entity.Phone
                }
            };

            var rowAffected = DapperRepositoryUtil.InsertIntoTable(CONTACT_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), parameters);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public IEnumerable<Contact> GetContactsByUniqueIds(IEnumerable<Guid> contactUniqueIds)
        {
            if (contactUniqueIds == null)
                throw new ArgumentNullException(nameof(contactUniqueIds));

            using (var context = new HuuHanhEntities())
            {
                return GetContactsByUniqueIds(contactUniqueIds, context.Contacts).ToList();
            }
        }

        internal IEnumerable<Contact> GetContactsByUniqueIds(IEnumerable<Guid> contactUniqueIds, IEnumerable<Contact> contacts)
        {
            return contacts.Where(c => contactUniqueIds.Contains(c.UniqueId));
        }

        public Guid? GetUniqueIdById(int contactId)
        {
            using (var context = new HuuHanhEntities())
            {
                return GetUniqueIdById(contactId, context.Contacts);
            }
        }

        internal Guid? GetUniqueIdById(int contactId, IEnumerable<Contact> contacts)
        {
            return contacts.FirstOrDefault(c => c.Id == contactId)?.UniqueId;
        }
    }
}
