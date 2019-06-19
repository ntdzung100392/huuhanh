using AutoMapper;
using HHCoApps.CMSWeb.Mappings;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers
{
    public class MappingComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddMaps(typeof(ContentInfoModelMappingProfile).Assembly);
            });

            config.CompileMappings();

            composition.Register(typeof(IMapper), config.CreateMapper());
        }
    }
}