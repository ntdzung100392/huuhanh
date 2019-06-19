using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Mappings
{
    public class ListingConfigurationMappingProfile : Profile
    {
        public ListingConfigurationMappingProfile()
        {
            CreateMap<ListingConfiguration, ItemListingSource>();
        }
    }
}