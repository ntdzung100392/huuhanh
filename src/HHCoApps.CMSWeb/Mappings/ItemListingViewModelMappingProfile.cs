using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ItemListingViewModelMappingProfile : Profile
    {
        public ItemListingViewModelMappingProfile()
        {
            CreateMap<ItemListing, ItemListingViewModel>()
                .ForMember(dst => dst.ViewMoreUrl, opt => opt.MapFrom(src => src.ViewMoreLink.Url))
                .ForMember(dst => dst.ViewMoreLabel, opt => opt.MapFrom(src => src.ViewMoreLabel ?? src.ViewMoreLink.Name));

            CreateMap<ItemListing, ItemListingSource>();
        }
    }
}