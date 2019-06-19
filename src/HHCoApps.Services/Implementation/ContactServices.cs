using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.Core;
using HHCoApps.Repository.Interfaces;
using HHCoApps.Services.Interfaces;
using HHCoApps.Services.Models;

namespace HHCoApps.Services.Implementation
{
    internal class ContactServices : IContactServices
    {
        private readonly IContactRepository _contactRepository;

        public ContactServices(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public ContactModel InsertContact(ContactModel model)
        {
            var entity = Mapper.Map<Contact>(model);
            entity.IsActive = true;
            entity.UniqueId = Guid.NewGuid();

            _contactRepository.InsertContact(entity);
            return Mapper.Map<ContactModel>(entity);
        }

        public IEnumerable<ContactModel> GetContactByUniqueIds(IEnumerable<Guid> contactUniqueIds)
        {
            if (!contactUniqueIds.Any())
                return Enumerable.Empty<ContactModel>();

            var contacts = _contactRepository.GetContactsByUniqueIds(contactUniqueIds);
            return Mapper.Map<IEnumerable<ContactModel>>(contacts);
        }

        public Guid? GetContactUniqueIdByContactId(int contactId)
        {
            return _contactRepository.GetUniqueIdById(contactId);
        }

        public void DeActiveContactInfoByContactUniqueIds()
        {

        }
    }
}
