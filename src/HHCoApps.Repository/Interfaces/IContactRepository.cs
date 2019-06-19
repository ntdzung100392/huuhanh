using System;
using System.Collections.Generic;
using HHCoApps.Core;

namespace HHCoApps.Repository.Interfaces
{
    public interface IContactRepository
    {
        void InsertContact(Contact entity);
        IEnumerable<Contact> GetContactsByUniqueIds(IEnumerable<Guid> contactUniqueIds);
        Guid? GetUniqueIdById(int contactId);
    }
}
