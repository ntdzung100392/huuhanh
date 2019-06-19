using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class MenuItemModelMappingProfile : Profile
    {
        public MenuItemModelMappingProfile()
        {
            CreateMap<MenuItem, MenuItemModel>()
                .ForMember(dst => dst.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dst => dst.NumberOfChildItemsPerColumn, opt => opt.MapFrom(src => src.NumberOfChildItemsPerColumn));

            CreateMap<Products, MenuItemModel>()
               .ForPath(dst => dst.Key, opt => opt.MapFrom(src => src.Id))
               .ForPath(dst => dst.Link.Url, opt => opt.MapFrom(src => src.Url))
               .ForPath(dst => dst.Link.Name, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RedirectUrl.Name) ? string.Empty : src.RedirectUrl.Name))
               .ForPath(dst => dst.Link.Target, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RedirectUrl.Target) ? string.Empty : src.RedirectUrl.Target))
               .ForPath(dst => dst.Title, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PageTitle) ? src.Name : src.PageTitle));

            CreateMap<Product, MenuItemModel>()
               .ForPath(dst => dst.Key, opt => opt.MapFrom(src => src.Id))
               .ForPath(dst => dst.Link.Url, opt => opt.MapFrom(src => src.Url))
               .ForPath(dst => dst.Link.Name, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RedirectUrl.Name) ? string.Empty : src.RedirectUrl.Name))
               .ForPath(dst => dst.Link.Target, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.RedirectUrl.Target) ? string.Empty : src.RedirectUrl.Target))
               .ForPath(dst => dst.Title, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PageTitle) ? src.Name : src.PageTitle));
        }
    }
}