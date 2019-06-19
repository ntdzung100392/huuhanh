using AutoMapper;
using AutoMapper.Configuration;
using HHCoApps.Services;
using HHCoApps.Services.Models;
using WareHouseApps.Models;

namespace WareHouseApps.Helper
{
    public class MapperInit
    { 
        /// <summary>
      /// Mapper configuration
      /// </summary>
        public MapperConfigurationExpression Configuration { get; } = new MapperConfigurationExpression();

        /// <summary>
        /// Initialize mapper
        /// </summary>
        public void Init()
        {
            Configuration.Mapping();
            // Static mapper

            Configuration.CreateMap<VendorModel, VendorViewModel>()
                .ForMember(d => d.VendorName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Contact.Address))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Contact.Email))
                .ForMember(d => d.Fax, o => o.MapFrom(s => s.Contact.Fax))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.Contact.Phone))
                .ForMember(d => d.TaxCode, o => o.MapFrom(s => s.Contact.TaxCode))
                .ReverseMap();
            Configuration.CreateMap<CategoryModel, CategoryViewModel>().ReverseMap();
            Configuration.CreateMap<ProductModel, ProductViewModel>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.VendorName, o => o.MapFrom(s => s.Vendor.Name))
                .ReverseMap();
            Configuration.CreateMap<ContactModel, ContactViewModel>().ReverseMap();

            Mapper.Initialize(Configuration);
        }
    }
}
