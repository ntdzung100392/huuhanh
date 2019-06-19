using System.Configuration;
using HHCoApps.CMSWeb.Helpers.CdnUrlResolvers;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers
{
    public class HelperComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["AzureBlobFileSystem.RootUrl:media"]))
            {
                composition.Register<ICdnUrlResolver, NullCdnUrlResolver>(Lifetime.Singleton);
            }
            else
            {
                composition.Register<ICdnUrlResolver, ImageProcessorCdnUrlResolver>(Lifetime.Singleton);
            }
        }
    }
}