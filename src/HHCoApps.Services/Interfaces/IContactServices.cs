using System;
using System.Collections.Generic;
using HHCoApps.Services.Models;

namespace HHCoApps.Services.Interfaces
{
    public interface IContactServices
    {
        ContactModel InsertContact(ContactModel model);
        IEnumerable<ContactModel> GetContactByUniqueIds(IEnumerable<Guid> contactUniqueIds);
        Guid? GetContactUniqueIdByContactId(int contactId);
    }
}
