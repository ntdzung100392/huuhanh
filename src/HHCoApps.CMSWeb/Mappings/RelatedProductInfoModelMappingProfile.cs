using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class RelatedProductInfoModelMappingProfile : Profile
    {
        public RelatedProductInfoModelMappingProfile()
        {
            CreateRelatedProductModelMapping<Product>();
            CreateRelatedProductModelMapping<Products>();
            CreateRelatedProductModelMapping<Article>();
            CreateRelatedProductModelMapping<Articles>();
            CreateRelatedProductModelMapping<Questions>();
            CreateRelatedProductModelMapping<LandingPage>();
            CreateRelatedProductModelMapping<SearchPage>();
        }

        private void CreateRelatedProductModelMapping<TSource>() where TSource : IContentBase, IPublishedContent, INavigationBase
        {
            CreateMap<TSource, RelatedProductModel>()
               .ForMember(dst => dst.ProductUrl, opt => opt.MapFrom(src => src.Url))
               .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.PageTitle));
        }
    }
}