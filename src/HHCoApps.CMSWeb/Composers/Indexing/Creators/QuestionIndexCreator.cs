using System.Collections.Generic;
using Examine;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Examine;
using Umbraco.Web.Search;

namespace HHCoApps.CMSWeb.Composers.Indexing.Creators
{
    public class QuestionIndexCreator : LuceneIndexCreator, IUmbracoIndexesCreator
    {
        private readonly IProfilingLogger _profilingLogger;
        private readonly ILocalizationService _localizationService;
        private readonly IPublicAccessService _publicAccessService;

        public QuestionIndexCreator(IProfilingLogger profilingLogger, ILocalizationService localizationService, IPublicAccessService publicAccessService)
        {
            _profilingLogger = profilingLogger;
            _localizationService = localizationService;
            _publicAccessService = publicAccessService;
        }

        public override IEnumerable<IIndex> Create()
        {
            var valueSetValidator = new ContentValueSetValidator(true, false, _publicAccessService, includeItemTypes: new [] { IndexConstants.QuestionNodeTypeAlias });

            var index = new UmbracoContentIndex(IndexConstants.QuestionIndex,
                CreateFileSystemLuceneDirectory(IndexConstants.QuestionIndex),
                new UmbracoFieldDefinitionCollection(),
                new StandardAnalyzer(Version.LUCENE_30),
                _profilingLogger,
                _localizationService,
                valueSetValidator);

            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("questionDetails", FieldDefinitionTypes.FullText));
            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("questionTitle", FieldDefinitionTypes.FullText));
            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("answeredDetails", FieldDefinitionTypes.FullText));

            index.TransformingIndexValues += GetIndexOnTransformingIndexValues;

            return new[] { index };
        }

        private void GetIndexOnTransformingIndexValues(object sender, IndexingItemEventArgs e)
        {
            var path = e.ValueSet.GetValue("path").ToString();
            path = path.Replace(",", " ");
            e.ValueSet.Add("searchablePath", path);
        }
    }
}