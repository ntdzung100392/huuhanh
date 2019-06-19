using System.Web;
using System.Web.Mvc;
using Umbraco.Web;

namespace HHCoApps.CMSWeb.Caching
{
    public static class CachedPartialExtensions
    {
        public static IHtmlString CachedPartial(this HtmlHelper htmlHelper, string partialViewName, int cachedSeconds, bool cacheByPage = false)
        {
            return htmlHelper.CachedPartial(partialViewName, null, cachedSeconds, cacheByPage);
        }
    }
}