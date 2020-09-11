using System.Reflection;
using HHCoApps.CMSWeb.Headspring;
using Umbraco.Core.Models.PublishedContent;

namespace HHCoApps.CMSWeb.Caching
{
    public class CachedContent : Enumeration<CachedContent, string>
    {
        public static CachedContent IntegrationContent = new CachedContent("ContentCache_Integration", "Integration")
        {
            XPath = "//integration",
            ContentTypeAlias = typeof(Umbraco.Web.PublishedModels.Integration).GetCustomAttribute<PublishedModelAttribute>().ContentTypeAlias
        };

        public static CachedContent NavigationManagementContent = new CachedContent("ContentCache_NavigationManagement", "Navigation Management")
        {
            XPath = "//navigationManagement",
            ContentTypeAlias = typeof(Umbraco.Web.PublishedModels.NavigationManagement).GetCustomAttribute<PublishedModelAttribute>().ContentTypeAlias
        };

        public static CachedContent RootQuestionsContent = new CachedContent("ContentCache_RootQuestions", "Questions")
        {
            XPath = "//home/questions",
            ContentTypeAlias = typeof(Umbraco.Web.PublishedModels.Questions).GetCustomAttribute<PublishedModelAttribute>().ContentTypeAlias
        };

        public static CachedContent HomeContent = new CachedContent("ContentCache_Home", "Home")
        {
            XPath = "//home",
            ContentTypeAlias = typeof(Umbraco.Web.PublishedModels.Home).GetCustomAttribute<PublishedModelAttribute>().ContentTypeAlias
        };

        public static CachedContent SearchPageContent = new CachedContent("ContentCache_SearchPage", "Search Result")
        {
            XPath = "//home/searchPage",
            ContentTypeAlias = typeof(Umbraco.Web.PublishedModels.SearchPage).GetCustomAttribute<PublishedModelAttribute>().ContentTypeAlias
        };

        public CachedContent(string value, string displayName) : base(value, displayName)
        {
        }

        public string XPath { get; set; }
        public string ContentTypeAlias { get; set; }
    }
}