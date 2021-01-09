using AutoMapper;
using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class ListingComponentService : IListingComponentService
    {
        private readonly IExamineManager _examineManager;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMapper _mapper;
        private readonly IContentIndexQueryService _contentIndexQueryService;
        private readonly IFilterCategoryService _filterCategoryService;

        public ListingComponentService(IExamineManager examineManager, UmbracoHelper umbracoHelper, IMapper mapper, IContentIndexQueryService contentIndexQueryService, IFilterCategoryService filterCategoryService)
        {
            _examineManager = examineManager;
            _umbracoHelper = umbracoHelper;
            _mapper = mapper;
            _contentIndexQueryService = contentIndexQueryService;
            _filterCategoryService = filterCategoryService;
        }

        public ListingComponentViewModel GetViewModel(ResponsiveItemListing model)
        {
            var viewModel = new ListingComponentViewModel();
            var filtersConfigurationModel = (FilterConfiguration)model.FilteringConfiguration;
            var filterListing = new List<string>();

            switch (model.LayoutPicker)
            {
                case "Two Columns":
                    viewModel.TemplateName = ReactLayoutConstant.TwoColumn.ToString();
                    break;
                case "Three Columns With Flexible Height Items":
                    viewModel.TemplateName = ReactLayoutConstant.ThreeColumnsWithFlexibleHeightItems.ToString();
                    break;       
                case "Three Columns With Attributes":
                    viewModel.TemplateName = ReactLayoutConstant.ThreeColumnsWithAttributes.ToString();
                    break;
                case "Four Columns":
                    viewModel.TemplateName = ReactLayoutConstant.FourColumns.ToString();
                    break;
                default:
                    viewModel.TemplateName = ReactLayoutConstant.TwoColumn.ToString();
                    break;
            }

            if (filtersConfigurationModel != null)
            {
                var listFilterConfiguration = from filter in filtersConfigurationModel.FilterCategories
                                              where model.FilteringBy.Any(filterBy => filterBy.Equals(filter.CategoryName, StringComparison.OrdinalIgnoreCase))
                                              select filter.CategoryName;

                var listFilterBy = from filterBy in model.FilteringBy
                                   where !filtersConfigurationModel.FilterCategories.Any(filter => filter.CategoryName.Equals(filterBy, StringComparison.OrdinalIgnoreCase))
                                   select filterBy;

                filterListing = listFilterConfiguration.Union(listFilterBy).ToList();
            }

            var arrangedFilter = filterListing.Any() ? filterListing : model.FilteringBy;
            viewModel.FilteringBy = arrangedFilter ?? Enumerable.Empty<string>();
            viewModel.IsEnabledFilter = model.EnableFilter;

            viewModel.DefaultFilteringBy = _filterCategoryService.GetDefaultFilterCategories(model.DefaultFilters);

            if (model.StarterNode != null)
            {
                viewModel.StarterNodeId = model.StarterNode.Id;
                viewModel.ContentType = model.StarterNode.ContentType.Alias;
            }

            viewModel.PrimaryFilterIds = Enumerable.Empty<string>();
            viewModel.NumberItemsPerPage = model.NumberItemsPerPage == 0 ? 9 : model.NumberItemsPerPage;
            viewModel.IncludedContentIds = model.AdditionalItems?.Select(x => x.Id);
            viewModel.SortingRequest = new SortingRequest
            {
                OrderType = string.IsNullOrEmpty(model.SortOrderBy) ? "Ascending" : model.SortOrderBy,
                SortBy = string.IsNullOrEmpty(model.SortType) ? SortBy.SortOrder.Value : model.SortType
            };

            viewModel.ViewMoreLabel = string.IsNullOrEmpty(model.ViewMoreLabel.Trim()) ? "View more" : model.ViewMoreLabel;
            viewModel.ViewDetailsLabel = string.IsNullOrEmpty(model.ViewDetailsLabel.Trim()) ? "View details" : model.ViewDetailsLabel;

            return viewModel;
        }

        public IEnumerable<ContentInfoGroupModel> GetContentInfoGroupedBySearchRequestAndGrandParentId(string indexName, SearchRequest searchRequest, string contentType, int pageSize = 10)
        {
            if (searchRequest == null)
                return Enumerable.Empty<ContentInfoGroupModel>();

            var grandParent = _umbracoHelper.Content(searchRequest.Paths);

            if (!string.IsNullOrEmpty(searchRequest.GroupByFilter))
            {
                var filterCategoryModels = _filterCategoryService.GetFilterCategories(new[] { searchRequest.GroupByFilter });
                if (filterCategoryModels.Any())
                {
                    if (grandParent != null)
                    {
                        searchRequest.Paths = grandParent.SelectMany(x => x.Children.Select(x => x.Id.ToString()));
                    }
                    return GetContentInfoGroupsByFilterValues(filterCategoryModels.First(), indexName, searchRequest, pageSize);
                }
            }

            var result = new List<ContentInfoGroupModel>();
            if (_examineManager.TryGetIndex(indexName, out _))
            {
                if (grandParent != null)
                {
                    foreach (var childItem in grandParent.SelectMany(x => x.Children))
                    {
                        var contentInfoGroupModel = _mapper.Map<ContentInfoGroupModel>(childItem);
                        GetContentInfoGroupModels(indexName, searchRequest, contentInfoGroupModel, new[] { childItem.Path }, Enumerable.Empty<string>(), contentType, pageSize);
                        if (contentInfoGroupModel.TotalChildItems != 0)
                        {
                            result.Add(contentInfoGroupModel);
                        }
                    }
                }

                if (searchRequest.IncludedContentIds.Any())
                {
                    var childNodeTypeAliases = new[]
                    {
                        IndexConstants.ArticleNodeTypeAlias, IndexConstants.ProductNodeTypeAlias, IndexConstants.LandingPageNodeTypeAlias
                    };

                    var additionalContentItems = _umbracoHelper.Content(searchRequest.IncludedContentIds);
                    var finalNodeContentItems = new List<IPublishedContent>();

                    foreach (var additionalContentItem in additionalContentItems)
                    {
                        if (childNodeTypeAliases.Contains(additionalContentItem.ContentType.Alias, StringComparer.InvariantCultureIgnoreCase))
                        {
                            finalNodeContentItems.Add(additionalContentItem);
                        }

                        if (additionalContentItem.Children.Any() && !childNodeTypeAliases.Contains(additionalContentItem.ContentType.Alias, StringComparer.InvariantCultureIgnoreCase))
                        {
                            result.AddRange(GetAdditionalContentInfoGroupModelsByParentPath(indexName, searchRequest, new[] { additionalContentItem }, pageSize));
                        }
                    }

                    result.AddRange(GetAdditionalContentInfoGroupModelByIds(indexName, searchRequest, finalNodeContentItems, pageSize));
                }
            }

            return result;
        }

        private IEnumerable<ContentInfoGroupModel> GetContentInfoGroupsByFilterValues(FilterCategoryModel filterCategoryModel, string indexName, SearchRequest searchRequest, int pageSize = 10)
        {
            var searchRequestModel = _mapper.Map<SearchRequestModel>(searchRequest);
            var result = new List<ContentInfoGroupModel>();
            var filterValues = filterCategoryModel.Items;

            foreach (var filterValue in filterValues)
            {
                var clonedSearchRequestModel = CombineGroupingFilterValuesAndDefaultCriteria(searchRequestModel, filterCategoryModel, filterValue);

                var pagingResult = _contentIndexQueryService.GetContentsBySearchRequestModel<ContentInfoModel>(indexName, clonedSearchRequestModel, string.Empty, 1, pageSize);
                if (pagingResult.TotalItemCount == 0)
                    continue;

                result.Add(new ContentInfoGroupModel
                {
                    Title = filterValue.Name,
                    ChildItems = pagingResult.Items,
                    TotalChildItems = pagingResult.TotalItemCount
                });
            }

            return result;
        }

        public long GetTotalItemsCountBySearchRequestModel(string indexName, SearchRequestModel searchRequestModel)
        {
            if (searchRequestModel == null)
                return 0;

            var includeNodeTypeAliases = new[]
            {
                IndexConstants.ProductNodeTypeAlias,
                IndexConstants.ArticleNodeTypeAlias,
                IndexConstants.LandingPageNodeTypeAlias
            };

            var searchResults = _contentIndexQueryService.GetContentsBySearchRequestModel(indexName, includeNodeTypeAliases, searchRequestModel);
            return searchResults.TotalItemCount;
        }

        private SearchRequestModel CombineGroupingFilterValuesAndDefaultCriteria(SearchRequestModel searchRequestModel, FilterCategoryModel filterCategoryModel, FilterItemModel filterValue)
        {
            var clonedSearchRequestModel = searchRequestModel.Clone();
            clonedSearchRequestModel.Paths = searchRequestModel.Paths.Concat(searchRequestModel.IncludedContentIds);

            if (!clonedSearchRequestModel.DefaultCriteria.Any(x => x.FilterValue.Equals(filterValue.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                var criteria = searchRequestModel.DefaultCriteria.ToList();
                criteria.Add(new FilterCriterionModel
                {
                    FilterCategoryKey = filterCategoryModel.Key,
                    FilterValue = filterValue.Name,
                    IsGroupAnd = true
                });
                clonedSearchRequestModel.DefaultCriteria = criteria;
            }

            return clonedSearchRequestModel;
        }

        private IEnumerable<ContentInfoGroupModel> GetAdditionalContentInfoGroupModelByIds(string indexName, SearchRequest searchRequest, IEnumerable<IPublishedContent> additionalContentItems, int pageSize)
        {
            if (!additionalContentItems.Any())
                return Enumerable.Empty<ContentInfoGroupModel>();

            var result = new List<ContentInfoGroupModel>();
            var additionalContentsGroupedByParent = additionalContentItems.GroupBy(x => x.Parent);
            foreach (var additionalContents in additionalContentsGroupedByParent)
            {
                var contentInfoGroupModel = _mapper.Map<ContentInfoGroupModel>(additionalContents.Key);
                GetContentInfoGroupModels(indexName, searchRequest, contentInfoGroupModel, additionalContents.Select(x => x.Id.ToString()), Enumerable.Empty<string>(), string.Empty, pageSize);

                if (contentInfoGroupModel.TotalChildItems != 0)
                {
                    result.Add(contentInfoGroupModel);
                }
            }

            return result;
        }

        private IEnumerable<ContentInfoGroupModel> GetAdditionalContentInfoGroupModelsByParentPath(string indexName, SearchRequest searchRequest, IEnumerable<IPublishedContent> additionalContentItems, int pageSize)
        {
            if (!additionalContentItems.Any())
                return Enumerable.Empty<ContentInfoGroupModel>();

            var result = new List<ContentInfoGroupModel>();
            foreach (var item in additionalContentItems)
            {
                var contentInfoGroupModel = _mapper.Map<ContentInfoGroupModel>(item);
                var contentType = item.ContentType.Alias;
                GetContentInfoGroupModels(indexName, searchRequest, contentInfoGroupModel, new[] { item.Path }, Enumerable.Empty<string>(), contentType, pageSize);

                if (contentInfoGroupModel.TotalChildItems != 0)
                {
                    result.Add(contentInfoGroupModel);
                }
            }

            return result;
        }

        private void GetContentInfoGroupModels(string indexName, SearchRequest searchRequest, ContentInfoGroupModel contentInfoGroupModel, IEnumerable<string> paths, IEnumerable<string> additionalIds, string contentType, int pageSize)
        {
            var clonedSearchRequest = searchRequest.Clone();
            clonedSearchRequest.IncludedContentIds = additionalIds;
            clonedSearchRequest.Paths = paths;

            var searchRequestModel = _mapper.Map<SearchRequestModel>(clonedSearchRequest);
            var pagingResult = _contentIndexQueryService.GetContentsBySearchRequestModel<ContentInfoModel>(indexName, searchRequestModel, contentType, 1, pageSize);
            contentInfoGroupModel.ChildItems = pagingResult.Items;
            contentInfoGroupModel.TotalChildItems = pagingResult.TotalItemCount;
        }

        public IEnumerable<string> GetChildNodesIdsByIds(IEnumerable<string> ids, SearchRequestModel searchRequestModel)
        {
            var orderedContentIds = new List<string>();
            var childNodeTypeAliases = new[]
            {
                IndexConstants.ArticleNodeTypeAlias, IndexConstants.ProductNodeTypeAlias, IndexConstants.LandingPageNodeTypeAlias
            };

            if (searchRequestModel.SortingModel == null || string.IsNullOrEmpty(searchRequestModel.SortingModel.SortBy))
            {
                foreach (var id in ids)
                {
                    var clonedSearchRequestModel = searchRequestModel.Clone();
                    clonedSearchRequestModel.Paths = new[] { id };
                    var childIds = _contentIndexQueryService.GetContentsBySearchRequestModel(IndexConstants.ContentIndex, childNodeTypeAliases, clonedSearchRequestModel).Select(x => x.Id);
                    orderedContentIds.AddRange(childIds);
                }

                return orderedContentIds.Distinct();
            }

            var initialSearchRequestModel = new SearchRequestModel
            {
                Paths = ids
            };

            return _contentIndexQueryService.GetContentsBySearchRequestModel(IndexConstants.ContentIndex, childNodeTypeAliases, initialSearchRequestModel).Select(x => x.Id).Distinct();
        }
    }

    public interface IListingComponentService
    {
        ListingComponentViewModel GetViewModel(ResponsiveItemListing itemListing);

        IEnumerable<ContentInfoGroupModel> GetContentInfoGroupedBySearchRequestAndGrandParentId(string indexName, SearchRequest searchRequest, string contentType, int pageSize = 10);

        IEnumerable<string> GetChildNodesIdsByIds(IEnumerable<string> ids, SearchRequestModel searchRequestModel);

        long GetTotalItemsCountBySearchRequestModel(string indexName, SearchRequestModel searchRequestModel);
    }
}