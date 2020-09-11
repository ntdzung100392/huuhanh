using AutoMapper;
using HHCoApps.Services.Models;
using WareHouseApps.Models;

namespace WareHouseApps.Helper
{
    public class CategoryModelMappingProfiles : Profile
    {
        public CategoryModelMappingProfiles()
        {
            CreateMap<CategoryModel, CategoryViewModel>().ReverseMap();
        }
    }
}