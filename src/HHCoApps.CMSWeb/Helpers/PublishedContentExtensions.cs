using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class PublishedContentExtensions
    {
        public static string GetUDI(this IPublishedContent content)
        {
            return new GuidUdi(Constants.UdiEntityType.Document, content.Key).EnsureClosed().ToString();
        }
    }
}