using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class CarouselInfoModelMappingProfile : Profile
    {
        public CarouselInfoModelMappingProfile()
        {
            CreateMap<CarouselInfo, CarouselInfoModel>()
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Link.Url))
                .ForMember(dst => dst.Target, opt => opt.MapFrom(src => src.Link.Target))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Link.Name))
                .ForMember(dst => dst.ImageAlt, opt => opt.MapFrom(src => src.Image.Name));
        }
    }
}