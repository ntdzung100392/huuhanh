using AutoMapper;
using HHCoApps.CMSWeb.Models;
using System;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ProductDetailSelectorMappingProfile : Profile
    {
        public ProductDetailSelectorMappingProfile()
        {
            CreateMap<Colour, ContentInfoModel>()
                .ForPath(dst => dst.Title, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ColourName)? src.ColourName: src.Name))
                .ForPath(dst => dst.ImageAlt, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ColourName) ? src.ColourName : src.Name))
                .ForPath(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ColourImage.Url));

            CreateMap<Timber, ContentInfoModel>()
                .ForPath(dst => dst.Title, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.TimberName) ? src.TimberName : src.Name))
                .ForPath(dst => dst.ImageAlt, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.TimberName) ? src.TimberName : src.Name))
                .ForPath(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.TimberImage.Url));
        }
    }
}