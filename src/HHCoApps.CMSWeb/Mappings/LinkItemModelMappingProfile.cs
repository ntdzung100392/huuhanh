using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class LinkItemModelMappingProfile : Profile
    {
        public LinkItemModelMappingProfile()
        {
            CreateMap<LinkItem, LinkItemModel>()
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Link.Url));

            CreateMap<Home, LinkItemModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Name));

            CreateBreadcrumbMapping<Product>();
            CreateBreadcrumbMapping<Products>();
            CreateBreadcrumbMapping<Questions>();
            CreateBreadcrumbMapping<LandingPage>();
            CreateBreadcrumbMapping<SearchPage>();
            CreateBreadcrumbMapping<Article>();
            CreateBreadcrumbMapping<Articles>();
            CreateBreadcrumbMapping<ContactUs>();
            CreateBreadcrumbMapping<Store>();
            CreateBreadcrumbMapping<Stores>();
        }

        private void CreateBreadcrumbMapping<TSource>() where TSource : IContentBase, IPublishedContent, INavigationBase
        {
            CreateMap<TSource, LinkItemModel>()
               .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Name));
        }
    }
}