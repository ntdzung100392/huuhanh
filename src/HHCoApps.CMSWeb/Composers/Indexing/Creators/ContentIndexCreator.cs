using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HHCoApps.CMSWeb.Helpers;
using Examine;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HtmlAgilityPack;
using Lucene.Net.Analysis.Standard;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Examine;
using Umbraco.Web;
using Umbraco.Web.Search;
using Version = Lucene.Net.Util.Version;

namespace HHCoApps.CMSWeb.Composers.Indexing.Creators
{
    public class ContentIndexCreator : LuceneIndexCreator, IUmbracoIndexesCreator
    {
        private readonly IProfilingLogger _profilingLogger;
        private readonly ILogger _logger;
        private readonly ILocalizationService _localizationService;
        private readonly IPublicAccessService _publicAccessService;
        private readonly IUmbracoContextFactory _context;

        public ContentIndexCreator(IProfilingLogger profilingLogger, ILogger logger, ILocalizationService localizationService, IPublicAccessService publicAccessService, IUmbracoContextFactory context)
        {
            _profilingLogger = profilingLogger;
            _logger = logger;
            _localizationService = localizationService;
            _publicAccessService = publicAccessService;
            _context = context;
        }

        public override IEnumerable<IIndex> Create()
        {
            var valueSetValidator = new ContentValueSetValidator(true, false, _publicAccessService, includeItemTypes: IndexConstants.AllNodeTypeAliases);

            var index = new UmbracoContentIndex(IndexConstants.ContentIndex,
                CreateFileSystemLuceneDirectory(IndexConstants.ContentIndex),
                new UmbracoFieldDefinitionCollection(),
                new StandardAnalyzer(Version.LUCENE_30, new HashSet<string>()), // Override the default stop words collection
                _profilingLogger,
                _localizationService,
                valueSetValidator);

            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("pageTitle", FieldDefinitionTypes.FullTextSortable));
            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("createDate", FieldDefinitionTypes.FullTextSortable));
            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("updateDate", FieldDefinitionTypes.FullTextSortable));
            index.FieldDefinitionCollection.AddOrUpdate(new FieldDefinition("description", FieldDefinitionTypes.FullText));
            index.TransformingIndexValues += GetIndexOnTransformingIndexValues;

            return new[] { index };
        }

        private void GetIndexOnTransformingIndexValues(object sender, IndexingItemEventArgs e)
        {
            var path = e.ValueSet.GetValue("path").ToString();
            path = path.Replace(",", " ");
            e.ValueSet.Add("searchablePath", path);

            var filters = e.ValueSet.GetValue("climateFilters")?.ToString();
            if (!string.IsNullOrEmpty(filters))
            {
                UpdateIndexForClimateFilters(e, filters);
            }

            var filterCategories = FilterCategory.GetAll();
            foreach (var filterCategory in filterCategories)
            {
                var filterValue = e.ValueSet.GetValue(filterCategory.PropertyAlias);
                if (filterValue == null)
                    continue;

                var filterValues = JsonConvert.DeserializeObject<string[]>(filterValue.ToString());
                var escapedFilterValues = filterValues.Select(x => x.NormalizeFilterValue());
                escapedFilterValues = escapedFilterValues.ReplaceStopWords();
                var filterWord = string.Join(" ", escapedFilterValues);
                e.ValueSet.Add(filterCategory.SearchablePropertyAlias, filterWord);
            }
            var gridValue = e.ValueSet.GetValue("__Raw_bodyText") ?? e.ValueSet.GetValue("__Raw_contentTemplate");
            if (gridValue != null)
            {
                try
                {
                    SetSearchableBodyTextIndex(gridValue.ToString(), e);
                }
                catch (Exception exception)
                {
                    _logger.Error<ContentIndexCreator>(exception);
                }
            }

            var gridTabs = e.ValueSet.GetValue("tabs");
            if (gridTabs != null)
            {
                var tabsValues = JsonConvert.DeserializeObject<dynamic>(gridTabs.ToString());
                foreach (var tabValue in tabsValues)
                {
                    try
                    {
                        SetSearchableBodyTextIndex(tabValue.content.ToString(), e);
                    }
                    catch (Exception exception)
                    {
                        _logger.Error<ContentIndexCreator>(exception);
                    }
                }
            }
        }

        private void SetSearchableBodyTextIndex(string gridValue, IndexingItemEventArgs e)
        {
            var gridBody = JsonConvert.DeserializeObject<dynamic>(gridValue);
            var document = new HtmlDocument();

            if (gridBody.sections == null)
                return;

            foreach (var section in gridBody.sections)
            {
                foreach (var row in section.rows)
                {
                    foreach (var area in row.areas)
                    {
                        foreach (var control in area.controls)
                        {
                            if (control != null && control.editor != null)
                            {
                                var controlEditorAlias = control.editor.alias?.ToString();

                                if ("rte".Equals(controlEditorAlias, StringComparison.OrdinalIgnoreCase))
                                {
                                    var controlValue = control.value.Value.ToString();
                                    document.LoadHtml(controlValue);
                                    var innerText = HttpUtility.HtmlDecode(document.DocumentNode.InnerText);
                                    e.ValueSet.Add("searchableBodyText", innerText);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateIndexForClimateFilters(IndexingItemEventArgs e, string filters)
        {
            var filterList = JsonConvert.DeserializeObject<List<FilterViewModel>>(filters);
            var filterValue = new StringBuilder();

            using (var umbracoContext = _context.EnsureUmbracoContext())
            {
                var content = umbracoContext.UmbracoContext.Content;
                foreach (var filter in filterList)
                {
                    if (!string.IsNullOrEmpty(filter.FilterSelectOption))
                    {
                        var filterUdis = filter.FilterSelectOption.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var filterUdi in filterUdis)
                        {
                            var udi = Udi.Parse(filterUdi);
                            var filterNode = content.GetById(udi);

                            if (filterNode.IsDocumentType("filterGroup"))
                            {
                                var filterGroupName = filterNode.GetProperty("groupName")?.Value().ToString().ReplaceDashAndEmptySpace();
                                filterValue.Append(filterGroupName);
                                filterValue.Append(" ");

                                var childFilterNodes = filterNode.Children;
                                if (childFilterNodes.Any())
                                {
                                    foreach (var childFilterNode in childFilterNodes)
                                    {
                                        var childFilterName = childFilterNode.GetProperty("filterName").Value().ToString().ReplaceDashAndEmptySpace();
                                        filterValue.Append($"{filterGroupName}{childFilterName}");
                                        filterValue.Append(" ");
                                    }
                                }
                            }
                            else
                            {
                                var parentFilterGroupNode = filterNode.Parent;
                                var filterGroupName = parentFilterGroupNode.GetProperty("groupName")?.Value().ToString().ReplaceDashAndEmptySpace();

                                if (!filterValue.ToString().Contains(filterGroupName))
                                {
                                    filterValue.Append(filterGroupName);
                                    filterValue.Append(" ");
                                }

                                var filterName = filterNode.GetProperty("filterName").Value().ToString().ReplaceDashAndEmptySpace();
                                var indexValue = $"{filterGroupName}{filterName}";
                                if (!filterValue.ToString().Contains(indexValue))
                                {
                                    filterValue.Append(indexValue);
                                    filterValue.Append(" ");
                                }
                            }
                        }
                    }
                }
            }

            e.ValueSet.Add("searchableClimateMonth", filterValue);
        }
    }
}