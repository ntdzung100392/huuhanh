using AutoMapper;
using AutoMapper.Configuration;
using HHCoApps.Services;
using HHCoApps.Services.Models;
using Ninject;
using Ninject.Modules;

namespace WareHouseApps.Helper
{
    public class MapperInit : NinjectModule
    {
        public override void Load()
        {

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
            // Add all profiles in current assembly
            cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }
    }
}
