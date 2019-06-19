using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class IconLinkModelMappingProfile : Profile
    {
        public IconLinkModelMappingProfile()
        {
            CreateMap<IconLink, IconLinkModel>()
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Link.Url))
                .ForMember(dst => dst.ImgUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dst => dst.Target, opt => opt.MapFrom(src => src.Link.Target));
        }
    }
}