using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace HHCoApps.CMSWeb.Caching
{
    public class CachingComponent : IComponent
    {
        private readonly IAppPolicyCache _runtimeCache;

        public CachingComponent(AppCaches appCaches)
        {
            _runtimeCache = appCaches.RuntimeCache;
        }

        public void Initialize()
        {
            ContentService.Published += ContentService_Published;
        }

        public void Terminate() { }

        private void ContentService_Published(IContentService sender, ContentPublishedEventArgs e)
        {
            foreach (var cachedContentKey in CachedContent.GetAll())
            {
                if (e.PublishedEntities.Any(x => x.ContentType.Alias == cachedContentKey.ContentTypeAlias))
                {
                    _runtimeCache.ClearByKey(cachedContentKey.Value);
                }
            }
        }
    }
}