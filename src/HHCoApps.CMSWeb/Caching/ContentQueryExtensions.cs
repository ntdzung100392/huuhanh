using System;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;

namespace HHCoApps.CMSWeb.Caching
{
    public static class ContentQueryExtensions
    {
        private static IAppPolicyCache _runtimeCache;
        private static IAppPolicyCache RuntimeCache
        {
            get
            {
                if (_runtimeCache == null)
                {
                    var appCache = Current.Factory.GetInstance<AppCaches>();
                    _runtimeCache = appCache.RuntimeCache;
                }

                return _runtimeCache;
            }
        }

        public static IPublishedContent ContentSingleFromCache(this IPublishedContentQuery contentQuery, CachedContent cachedContent)
        {
            return RuntimeCache.GetCacheItem(cachedContent.Value, () =>
            {
                var content = contentQuery.ContentSingleAtXPath(cachedContent.XPath);
                return content;
            }, TimeSpan.FromMinutes(5));
        }

        public static IPublishedContent ContentSingleFromCache(this IPublishedContentCache contentQuery, CachedContent cachedContent)
        {
            return RuntimeCache.GetCacheItem(cachedContent.Value, () =>
            {
                var content = contentQuery.GetSingleByXPath(cachedContent.XPath);
                return content;
            }, TimeSpan.FromMinutes(5));
        }
    }
}