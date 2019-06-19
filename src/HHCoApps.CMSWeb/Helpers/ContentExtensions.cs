using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class ContentExtensions
    {
        public static IEnumerable<IPublishedContent> GetImages(this IEnumerable<IPublishedContent> publishedContents)
        {
            return publishedContents.Where(x => x.IsImage());
        }

        public static bool IsImage(this IPublishedContent content)
        {
            var imageExtensions = new[] { "jpg", "png", "jpeg", "gif" };
            return content.ItemType == PublishedItemType.Media &&
                   imageExtensions.Contains(content.GetProperty("umbracoExtension")?.Value(defaultValue: string.Empty)?.ToLower());
        }

        public static bool IsDisplayInNavigation(this IPublishedContent content)
        {
            var navigationBase = content as INavigationBase;
            return !navigationBase?.UmbracoNavihide ?? true;
        }

        public static TContent Content<TContent>(this UmbracoHelper umbracoHelper, string id) where TContent : PublishedContentModel
        {
            return (TContent)umbracoHelper.Content(id);
        }
    }
}