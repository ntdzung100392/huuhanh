using AutoMapper;
using HHCoApps.Services.Models;
using WareHouseApps.Models;

namespace WareHouseApps.Helper
{
    public class VendorModelMappingProfiles : Profile
    {
        public VendorModelMappingProfiles()
        {
            CreateMap<VendorModel, VendorViewModel>()
                .ForMember(d => d.VendorName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Contact.Address))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Contact.Email))
                .ForMember(d => d.Fax, o => o.MapFrom(s => s.Contact.Fax))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Contact.Phone))
                .ForMember(d => d.TaxCode, o => o.MapFrom(s => s.Contact.TaxCode))
                .ReverseMap();
        }
    }
}