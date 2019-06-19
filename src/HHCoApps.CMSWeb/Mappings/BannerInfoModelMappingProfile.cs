using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class BannerInfoModelMappingProfile : Profile
    {
        public BannerInfoModelMappingProfile()
        {
            CreateMap<BannerComponent, BannerComponentViewModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.CaptionTitle))
                .ForMember(dst => dst.LeftImageUrl, opt => opt.MapFrom(src => src.LeftImage.Url))
                .ForMember(dst => dst.LeftImageTitle, opt => opt.MapFrom(src => src.LeftLinkTitle))
                .ForMember(dst => dst.LeftUrl, opt => opt.MapFrom(src => src.LeftLink.Url))
                .ForMember(dst => dst.RightImageUrl, opt => opt.MapFrom(src => src.RightImage.Url))
                .ForMember(dst => dst.RightImageTitle, opt => opt.MapFrom(src => src.RightLinkTitle))
                .ForMember(dst => dst.RightUrl, opt => opt.MapFrom(src => src.RightLink.Url));
        }
    }
}