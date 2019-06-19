using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class SeoMetadataService : ISeoMetadataService
    {
        private readonly UmbracoHelper _umbracoHelper;
        public SeoMetadataService(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public SeoMetadataViewModel GetSeoMetadata(IPublishedContent publishedContent)
        {
            var title = string.Empty;
            var description = string.Empty;
            var keyword = string.Empty;

            if (publishedContent is Home)
            {
                var content = (NavigationManagement)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.NavigationManagementContent);
                title = content?.SiteName ?? string.Empty;
                description = content?.SiteDescription ?? string.Empty;
            }
            else
            {
                var navigation = (INavigationBase)publishedContent;
                description = navigation.SeoMetaDescription;

                if (publishedContent is IContentBase content)
                {
                    title = string.IsNullOrWhiteSpace(content.MetadataTitle) ? content.PageTitle : content.MetadataTitle;
                    description = string.IsNullOrWhiteSpace(description) ? content.Description : description;
                    keyword = string.Join(",", navigation.Keywords);
                }
            }

            return new SeoMetadataViewModel
            {
                Title = title,
                Keyword = keyword,
                Description = description
            };
        }
    }

    public interface ISeoMetadataService
    {
        SeoMetadataViewModel GetSeoMetadata(IPublishedContent publishedContent);
    }
}