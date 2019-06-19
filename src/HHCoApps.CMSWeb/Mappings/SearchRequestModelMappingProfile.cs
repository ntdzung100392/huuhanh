using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services.Models;

namespace HHCoApps.CMSWeb.Mappings
{ 
    public class SearchRequestModelMappingProfile : Profile
    {
        public SearchRequestModelMappingProfile()
        {
            CreateMap<SortingRequest, SortingModel>();
            CreateMap<FilterCriterion, FilterCriterionModel>();
            CreateMap<SearchRequest, SearchRequestModel>()
                .ForMember(dst => dst.Paths, opt => opt.MapFrom(src => src.Paths))
                .ForMember(dst => dst.SortingModel, opt => opt.MapFrom(src => src.SortingRequest));
        }
    }
}