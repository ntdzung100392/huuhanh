using HHCoApps.CMSWeb.Controllers;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers
{
    public class RegisterApiControllerComposer : IComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ContentsController>(Lifetime.Request);
        }
    }
}