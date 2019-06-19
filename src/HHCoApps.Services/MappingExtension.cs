using AutoMapper.Configuration;
using HHCoApps.Core;
using HHCoApps.Services.Models;

namespace HHCoApps.Services
{
    public static class MappingExtension
    {
        /// <summary>
        /// Initialize mapper
        /// </summary>
        public static void Mapping(this MapperConfigurationExpression Configuration)
        {
            Configuration.CreateMap<User, UserModel>().ReverseMap();
            Configuration.CreateMap<Category, CategoryModel>().ReverseMap();
            Configuration.CreateMap<Vendor, VendorModel>().ReverseMap();
            Configuration.CreateMap<Product, ProductModel>().ReverseMap();
            Configuration.CreateMap<Contact, ContactModel>().ReverseMap();
        }
    }
}
