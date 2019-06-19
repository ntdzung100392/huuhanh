using System;
using System.Linq;
using System.Reflection;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Composers.Indexing
{
    public class IndexConstants
    {
        public const string ContentIndex = "ContentIndex";
        public const string QuestionIndex = "QuestionIndex";

        public const string ProductNodeTypeAlias = "product";
        public const string ArticleNodeTypeAlias = "article";
        public const string LandingPageNodeTypeAlias = "landingPage";
        public const string QuestionNodeTypeAlias = "question";
        public static string[] ProductNodeTypeAliases = { "products", ProductNodeTypeAlias };
        public static string[] ArticleNodeTypeAliases = { "articles", ArticleNodeTypeAlias };
        public static string[] AllNodeTypeAliases = ProductNodeTypeAliases
            .Union(ArticleNodeTypeAliases).Union(OtherNodeTypeAliases).ToArray();

        private static string[] _otherNodeTypeAliases;

        public static string[] OtherNodeTypeAliases
        {
            get
            {
                if (_otherNodeTypeAliases != null)
                    return _otherNodeTypeAliases;

                var excludeTypeAliases = new[]
                {
                    QuestionNodeTypeAlias, 
                    GetAliasName(typeof(ContentBase)), 
                    GetAliasName(typeof(SearchPage))
                }.Union(ProductNodeTypeAliases).Union(ArticleNodeTypeAliases);

                var contentTypes = typeof(Home).Assembly.GetTypes()
                    .Where(x => x.Implements<IContentBase>() && !x.IsAbstract && x.IsClass);

                _otherNodeTypeAliases = contentTypes.Select(GetAliasName).Except(excludeTypeAliases).ToArray();

                return _otherNodeTypeAliases;
            }
        }

        private static string GetAliasName(Type contentType)
        {
            var attribute = contentType.GetCustomAttribute<PublishedModelAttribute>();
            return attribute.ContentTypeAlias;
        }
    }
}