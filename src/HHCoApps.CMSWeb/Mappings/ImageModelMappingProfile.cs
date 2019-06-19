using AutoMapper;
using HHCoApps.CMSWeb.Models;
using System.Linq;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ImageModelMappingProfile : Profile
    {
        public ImageModelMappingProfile()
        {
            CreateMap<Image, ImageModel>()
               .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Url))
               .ForMember(dst => dst.ProductImageSize, opt => opt.MapFrom(src => src.ProductSize != null && src.ProductSize.Any() ? src.ProductSize.First() : string.Empty))
               .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}