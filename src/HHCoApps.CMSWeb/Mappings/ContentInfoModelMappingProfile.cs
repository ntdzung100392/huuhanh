using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ContentInfoModelMappingProfile : Profile
    {
        public ContentInfoModelMappingProfile()
        {
            CreateMap<ContentInfo, ContentInfoModel>()
                .ForMember(dst => dst.Udi, opt => opt.MapFrom(src => src.Link.Udi))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Link.Url))
                .ForMember(dst => dst.Target, opt => opt.MapFrom(src => src.Link.Target))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dst => dst.ImageWidth, opt => opt.MapFrom(src => src.Image.GetProperty("umbracoWidth").GetValue(default, default)))
                .ForMember(dst => dst.ImageHeight, opt => opt.MapFrom(src => src.Image.GetProperty("umbracoHeight").GetValue(default, default)))
                .ForMember(dst => dst.ImageAlt, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Title) ? src.Title : src.Image.Name));

            CreateMap<ProductInfo, RelatedProductModel>()
                .ForMember(dst => dst.ProductUrl, opt => opt.MapFrom(src => src.Link.Url))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Colour.GetProperty("colourName").GetValue(default, default)))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Label) ? src.Link.GetProperty("pageTitle").GetValue(default, default) : src.Label))
                .ForMember(dst => dst.ProductSubCategory, opt => opt.MapFrom(src => src.Link.GetProperty("productSubCategory").GetValue(default, default)));

            CreateContentInfoMapping<Product>();
            CreateContentInfoMapping<Products>();
            CreateContentInfoMapping<LandingPage>();
            CreateContentInfoMapping<SearchPage>();
            CreateContentInfoMapping<Store>();
            CreateContentInfoMapping<Stores>();

            CreateMap<ContentInfoModel, ContentInfoGroupModel>();
            CreateMap<Home, ContentInfoModel>();
        }

        private void CreateContentInfoMapping<TSource>() where TSource : IContentBase, IPublishedContent, INavigationBase
        {
            CreateMap<TSource, ContentInfoModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ListingTitle) ? src.PageTitle : src.ListingTitle))
                .ForMember(dst => dst.Caption, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.ParentTitle, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.GetProperty("pageTitle").GetValue(default, default) : string.Empty))
                .ForMember(dst => dst.ParentUrl, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Url : string.Empty))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault().Url))
                .ForMember(dst => dst.ImageWidth, opt => opt.MapFrom(src => src.Images.FirstOrDefault().GetProperty("umbracoWidth").GetValue(default, default)))
                .ForMember(dst => dst.ImageHeight, opt => opt.MapFrom(src => src.Images.FirstOrDefault().GetProperty("umbracoHeight").GetValue(default, default)))
                .ForMember(dst => dst.ImageAlt, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ListingTitle) ? src.PageTitle : src.ListingTitle));

            CreateMap<TSource, ContentInfoGroupModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ListingTitle) ? src.PageTitle : src.ListingTitle))
                .ForMember(dst => dst.Caption, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault().Url))
                .ForMember(dst => dst.ImageWidth, opt => opt.MapFrom(src => src.Images.FirstOrDefault().GetProperty("umbracoWidth").GetValue(default, default)))
                .ForMember(dst => dst.ImageHeight, opt => opt.MapFrom(src => src.Images.FirstOrDefault().GetProperty("umbracoHeight").GetValue(default, default)))
                .ForMember(dst => dst.ImageAlt, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ListingTitle) ? src.PageTitle : src.ListingTitle));
        }
    }
}