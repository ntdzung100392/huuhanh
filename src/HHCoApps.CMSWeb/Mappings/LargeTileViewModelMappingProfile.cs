using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class LargeTileViewModelMappingProfile : Profile
    {
        public LargeTileViewModelMappingProfile()
        {
            CreateMap<LargeTile, LargeTileViewModel>()
                .ForMember(dst => dst.BackgroundColor, opt => opt.MapFrom(src => $"#{src.BackgroundColor}"))
                .ForMember(dst => dst.FontColor, opt => opt.MapFrom(src => $"#{src.FontColor}"))
                .ForMember(dst => dst.NavigationUrl, opt => opt.MapFrom(src => src.SitePageOrExternalLink.Url))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageSource.Url));
        }
    }
}