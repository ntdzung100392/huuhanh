using AutoMapper;
using HHCoApps.Services.Models;
using WareHouseApps.Models;

namespace WareHouseApps.Helper
{
    public class ContactModelMappingProfiles : Profile
    {
        public ContactModelMappingProfiles()
        {
            CreateMap<ContactModel, ContactViewModel>().ReverseMap();
        }
    }
}