using AutoMapper;
using System.Linq;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ImageModelMappingProfile : Profile
    {
        public ImageModelMappingProfile()
        {
            CreateMap<Image, ImageModel>()
               .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Url))
               .ForMember(dst => dst.ImageWidth, opt => opt.MapFrom(src => src.GetProperty("umbracoWidth").GetValue(default, default)))
               .ForMember(dst => dst.ImageHeight, opt => opt.MapFrom(src => src.GetProperty("umbracoHeight").GetValue(default, default)))
               .ForMember(dst => dst.ProductImageSize, opt => opt.MapFrom(src => src.ProductSize != null && src.ProductSize.Any() ? src.ProductSize.First() : string.Empty))
               .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}