using Umbraco.Core;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Caching
{
    public class CachingComposer: IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<CachingComponent>();
        }
    }
}