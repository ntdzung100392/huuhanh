using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class StoreGeoJSONMappingProfile : Profile
    {
        public StoreGeoJSONMappingProfile()
        {
            CreateMap<Store, StoreInformation>()
                .ForPath(dst => dst.Properties.Name, opt => opt.MapFrom(src => src.StoreName))
                .ForPath(dst => dst.Properties.StoreId, opt => opt.MapFrom(src => StringExtension.Slugify(src.StoreName)))
                .ForPath(dst => dst.Properties.StoreUrl, opt => opt.MapFrom(src => src.Url))
                .ForPath(dst => dst.Properties.Address.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dst => dst.Properties.Address.Fax, opt => opt.MapFrom(src => src.Fax));
        }
    }
}