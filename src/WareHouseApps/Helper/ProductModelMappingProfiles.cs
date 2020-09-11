using AutoMapper;
using HHCoApps.Services.Models;
using WareHouseApps.Models;

namespace WareHouseApps.Helper
{
    public class ProductModelMappingProfiles : Profile
    {
        public ProductModelMappingProfiles()
        {
            CreateMap<ProductModel, ProductViewModel>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.VendorName, o => o.MapFrom(s => s.Vendor.Name))
                .ReverseMap();
        }
    }
}