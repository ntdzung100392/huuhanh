using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class NavigationInfoModelMappingProfile : Profile
    {
        public NavigationInfoModelMappingProfile()
        {
            CreateMap<Home, NavigationModel>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Url));

            CreateNavigationModelMapping<Product>();
            CreateNavigationModelMapping<Products>();
            CreateNavigationModelMapping<Questions>();
            CreateNavigationModelMapping<LandingPage>();
            CreateNavigationModelMapping<SearchPage>();
            CreateNavigationModelMapping<Article>();
            CreateNavigationModelMapping<Articles>();
            CreateNavigationModelMapping<ContactUs>();
            CreateNavigationModelMapping<Store>();
            CreateNavigationModelMapping<Stores>();
        }

        private void CreateNavigationModelMapping<TSource>() where TSource : IContentBase, IPublishedContent, INavigationBase
        {
            CreateMap<TSource, NavigationModel>()
               .ForMember(dst => dst.Uid, opt => opt.MapFrom(src => src.Key))
               .ForMember(dst => dst.Title, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.NavigationTitle) ? src.NavigationTitle : src.PageTitle))
               .ForMember(dst => dst.ChildNodes, opt => opt.MapFrom(src => src.Children.Where(x => x.IsDisplayInNavigation())));
        }
    }
}