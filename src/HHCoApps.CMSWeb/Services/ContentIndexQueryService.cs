using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using Examine;
using Examine.Search;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Services.Models;
using Umbraco.Web;

namespace HHCoApps.CMSWeb.Services
{
    public class ContentIndexQueryService : IContentIndexQueryService
    {
        private readonly IExamineManager _examineManager;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMapper _mapper;

        public ContentIndexQueryService(IExamineManager examineManager, UmbracoHelper umbracoHelper, IMapper mapper)
        {
            _examineManager = examineManager;
            _umbracoHelper = umbracoHelper;
            _mapper = mapper;
        }

        public PagingResult<TModel> GetPagingResult<TModel>(string indexName, string[] includeNodeTypeAliases, SearchRequestModel searchRequestModel, int pageNumber, int pageSize = 10)
        {
            var result = new PagingResult<TModel>();
            if (_examineManager.TryGetIndex(indexName, out var index))
            {
                var searcher = index.GetSearcher();
                var criteria = searcher.CreateQuery();
                var parsedSearchTerm = ParseSearchTerm(searchRequestModel.SearchText);
                var searchResults = criteria.GroupedOr(new[] {"__NodeTypeAlias"}, includeNodeTypeAliases);

                if (searchRequestModel.Paths.Any())
                {
                    var searchPaths = searchRequestModel.Paths.Select(searchPath => searchPath.Replace(",", " ")).ToArray();
                    searchResults = searchResults.And().Group(query =>
                    {
                        INestedBooleanOperation nestedBooleanOperation = null;
                        if (searchPaths.Any())
                        {
                            nestedBooleanOperation = query.GroupedOr(new[] {"searchablePath", "id"}, searchPaths);
                            query = nestedBooleanOperation.Or();
                        }

                        return nestedBooleanOperation;
                    });
                }

                searchResults = searchResults.And().GroupedOr(new[] { "nodeName", "pageTitle", "description", "searchableBodyText" }, parsedSearchTerm);

                var pagedList = searchResults.Execute().Select(x => x.Id).ToPagedList(pageNumber, pageSize);
                var contents = _umbracoHelper.Content(pagedList);
                var models = _mapper.Map<IEnumerable<TModel>>(contents);

                MapPagingResult(result, models, pagedList);
            }

            return result;
        }

        public ISearchResults GetContentsBySearchRequestModel(string indexName, string[] includeNodeTypeAliases, SearchRequestModel searchRequestModel)
        {
            if (_examineManager.TryGetIndex(indexName, out var index))
            {
                var searcher = index.GetSearcher();
                var criteria = searcher.CreateQuery();

                var operation = criteria.GroupedOr(new[] { "__NodeTypeAlias" }, includeNodeTypeAliases);

                if (searchRequestModel.Paths.Any() || searchRequestModel.IncludedContentIds.Any())
                {
                    var searchPaths = searchRequestModel.Paths.Select(searchPath => searchPath.Replace(",", " ")).ToArray();
                    operation = operation.And().Group(query =>
                        {
                            INestedBooleanOperation nestedBooleanOperation = null;
                            if (searchPaths.Any())
                            {
                                nestedBooleanOperation = query.GroupedOr(new[] {"searchablePath", "id"}, searchPaths);
                                query = nestedBooleanOperation.Or();
                            }

                            if (searchRequestModel.IncludedContentIds.Any())
                            {
                                nestedBooleanOperation = query.GroupedOr(new[] {"id"}, searchRequestModel.IncludedContentIds.ToArray());
                            }

                            return nestedBooleanOperation;
                        });
                }

                if (!string.IsNullOrEmpty(searchRequestModel.SearchText))
                {
                    operation = operation.And().NativeQuery(searchRequestModel.SearchText);
                }

                if (searchRequestModel.DefaultCriteria.Any())
                {
                    var groupByKey = searchRequestModel.DefaultCriteria.GroupBy(x => x.FilterCategoryKey);
                    foreach (var group in groupByKey)
                    {
                        SetQueryCriteria(group, operation);
                    }
                }

                if (searchRequestModel.Criteria.Any())
                {
                    var groupByKey = searchRequestModel.Criteria.Where(x => !x.IsPrimaryFilter && !x.IsPrimarySubFilter).GroupBy(x => x.FilterCategoryKey);
                    foreach (var group in groupByKey)
                    {
                        SetQueryCriteria(group, operation);
                    }

                    operation = GetQueryCriteriaByPrimaryFilter(searchRequestModel.Criteria, operation);
                }

                SetOrderOperation(searchRequestModel, operation);

                return operation.Execute();
            }

            return EmptySearchResults.Instance;
        }

        private void SetOrderOperation(SearchRequestModel searchRequestModel, IBooleanOperation operation)
        {
            if (searchRequestModel.SortingModel != null)
            {
                var sortBy = SortBy.FromDisplayName(searchRequestModel.SortingModel.SortBy);
                if (sortBy != null)
                {
                    var orderType = string.IsNullOrEmpty(searchRequestModel.SortingModel.OrderType) ? OrderType.Ascending : OrderType.FromDisplayName(searchRequestModel.SortingModel.OrderType);
                    if (orderType.Equals(OrderType.Descending))
                    {
                        operation.OrderByDescending(new SortableField(sortBy.Value));
                    }
                    else
                    {
                        operation.OrderBy(new SortableField(sortBy.Value));
                    }
                }
                else
                {
                    operation.OrderBy(new SortableField(SortBy.SortOrder.Value, SortType.Int));
                }
            }
            else
            {
                operation.OrderBy(new SortableField(SortBy.SortOrder.Value, SortType.Int));
            }
        }

        private void SetQueryCriteria(IGrouping<string, FilterCriterionModel> group, IBooleanOperation operation)
        {
            var filterCategory = FilterCategory.FromValue(group.Key);

            FilterByGroupOrQueryCriteria(group, filterCategory, operation);

            FilterByGroupAndQueryCriteria(group, filterCategory, operation);
        }

        private void FilterByGroupOrQueryCriteria(IGrouping<string, FilterCriterionModel> group, FilterCategory filterCategory, IBooleanOperation operation)
        {
            var groupOrFilterValues = group.Where(x => !x.IsGroupAnd).Select(x => x.FilterValue.NormalizeFilterValue()).ToArray();
            if (groupOrFilterValues.Any())
            {
                var escapedGroupOrFilterValues = groupOrFilterValues.ReplaceStopWords();
                operation.And().GroupedOr(new[] { filterCategory.SearchablePropertyAlias }, escapedGroupOrFilterValues.ToArray());
            }
        }

        private void FilterByGroupAndQueryCriteria(IGrouping<string, FilterCriterionModel> group, FilterCategory filterCategory, IBooleanOperation operation)
        {
            var groupAndFilterValues = group.Where(x => x.IsGroupAnd).Select(x => x.FilterValue.NormalizeFilterValue()).ToArray();
            if (groupAndFilterValues.Any())
            {
                var escapedGroupAndFilterValues = groupAndFilterValues.ReplaceStopWords();
                operation.And().GroupedAnd(new[] { filterCategory.SearchablePropertyAlias }, escapedGroupAndFilterValues.ToArray());
            }
        }

        private IBooleanOperation GetQueryCriteriaByPrimaryFilter(IEnumerable<FilterCriterionModel> criteria, IBooleanOperation operation)
        {
            var primaryGroup = criteria.Where(x => x.IsPrimaryFilter || x.IsPrimarySubFilter).GroupBy(x => x.FilterCategoryKey);
            foreach (var group in primaryGroup)
            {
                var queryValueBuilder = new List<string>();
                var primaryFilterValue = group.SingleOrDefault(x => x.IsPrimaryFilter)?.FilterValue ?? group.First().FilterCategoryKey;

                if (group.Any(x => x.IsPrimarySubFilter))
                {
                    var primarySubFilterValues = group.Where(x => x.IsPrimarySubFilter).Select(x => x.FilterValue.ReplaceDashAndEmptySpace());
                    foreach (var primarySubFilterValue in primarySubFilterValues)
                    {
                        var indexValue = $"{primaryFilterValue.ReplaceDashAndEmptySpace()}{primarySubFilterValue}";
                        queryValueBuilder.Add(indexValue);
                    }
                }
                else
                {
                    queryValueBuilder.Add(primaryFilterValue.ReplaceDashAndEmptySpace());
                }

                operation = operation.And().GroupedOr(new[] { "searchableClimateMonth" }, queryValueBuilder.ToArray());
            }

            return operation;
        }

        public PagingResult<TModel> GetContentsBySearchRequestModel<TModel>(string indexName, SearchRequestModel searchRequestModel, string contentType, int pageNumber, int pageSize = 10)
        {
            searchRequestModel.NodeTypeAlias = GetNodeTypeAliasesByParentContentType(contentType);

            var nodeTypeAlias = string.IsNullOrEmpty(searchRequestModel.NodeTypeAlias)
                ? new[]
                {
                    IndexConstants.ProductNodeTypeAlias,
                    IndexConstants.ArticleNodeTypeAlias,
                    IndexConstants.LandingPageNodeTypeAlias
                }
                : new[] { searchRequestModel.NodeTypeAlias };

            var searchResults = GetContentsBySearchRequestModel(indexName, nodeTypeAlias, searchRequestModel);
            var itemIds = searchResults.Select(x => x.Id).ToList();

            if (string.IsNullOrEmpty(searchRequestModel.SortingModel?.SortBy) && searchRequestModel.IncludedContentIds.Any())
            {
                itemIds = itemIds.Where(id => !searchRequestModel.IncludedContentIds.Contains(id))
                    .Union(searchRequestModel.IncludedContentIds).ToList();
            }

            var result = new PagingResult<TModel>();
            if (itemIds.Any())
            {
                itemIds = itemIds.Distinct().ToList();
                var pagedList = itemIds.ToPagedList(pageNumber, pageSize);
                var contents = _umbracoHelper.Content(pagedList);
                var models = _mapper.Map<IEnumerable<TModel>>(contents);

                MapPagingResult(result, models, pagedList);
            }

            return result;
        }

        public string GetNodeTypeAliasesByParentContentType(string contentType)
        {
            switch (contentType)
            {
                case "products":
                    return IndexConstants.ProductNodeTypeAlias;
                case "articles":
                    return IndexConstants.ArticleNodeTypeAlias;
                case "landingPage":
                    return IndexConstants.LandingPageNodeTypeAlias;
                default:
                    return string.Empty;
            }
        }

        private static void MapPagingResult<TModel>(PagingResult<TModel> result, IEnumerable<TModel> models, IPagedList<string> pagedList)
        {
            result.Items = models;
            result.TotalItemCount = pagedList.TotalItemCount;
            result.IsLastPage = pagedList.IsLastPage;
            result.PageNumber = pagedList.PageNumber;
            result.PageSize = pagedList.PageSize;
        }

        private string ParseSearchTerm(string searchTerm)
        {
            var splitWords = searchTerm.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var newSearchTerm = string.Join(" ", splitWords);
            return newSearchTerm;
        }
    }

    public interface IContentIndexQueryService
    {
        PagingResult<TModel> GetPagingResult<TModel>(string indexName, string[] includeNodeTypeAliases, SearchRequestModel searchRequestModel, int pageNumber, int pageSize = 10);
        PagingResult<TModel> GetContentsBySearchRequestModel<TModel>(string indexName, SearchRequestModel searchRequestModel, string contentType, int pageNumber, int pageSize = 10);
        ISearchResults GetContentsBySearchRequestModel(string indexName, string[] includeNodeTypeAliases, SearchRequestModel searchRequestModel);
        string GetNodeTypeAliasesByParentContentType(string contentType);
    }
}