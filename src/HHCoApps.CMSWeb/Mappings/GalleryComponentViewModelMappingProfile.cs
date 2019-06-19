using AutoMapper;
using HHCoApps.CMSWeb.Services.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class GalleryComponentViewModelMappingProfile : Profile
    {
        public GalleryComponentViewModelMappingProfile()
        {
            CreateMap<GalleryComponent, GalleryComponentViewModel>()
                .ForMember(dst => dst.UId, opt => opt.MapFrom(src => src.Key))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src =>!string.IsNullOrEmpty(src.Label.Trim()) ? src.Label.Trim() : string.Empty))
                .ForMember(dst => dst.ViewMoreUrl, opt => opt.MapFrom(src => src.ViewMoreLink.Url))
                .ForMember(dst => dst.ViewMoreLabel, opt => opt.MapFrom(src => src.ViewMoreLabel ?? src.ViewMoreLink.Name))
                .ForMember(dst => dst.ViewMoreTarget, opt => opt.MapFrom(src => src.ViewMoreLink.Target));
        }
    }
}