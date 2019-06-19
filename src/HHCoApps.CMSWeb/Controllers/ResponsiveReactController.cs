using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Services;
using HHCoApps.CMSWeb.Services.Models;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class ResponsiveReactController : UmbracoApiController
    {
        private readonly IMapper _mapper;
        private readonly IListingComponentService _listingComponentService;
        private readonly IFilterCategoryService _filterCategoryService;

        public ResponsiveReactController(IMapper mapper, IListingComponentService listingComponentService, IFilterCategoryService filterCategoryService)
        {
            _mapper = mapper;
            _listingComponentService = listingComponentService;
            _filterCategoryService = filterCategoryService;
        }

        [HttpGet]
        public IEnumerable<PrimaryFilterCategoryModel> GetPrimaryFilterCategoryModels([FromUri] string[] primaryFilters)
        {
            if (!primaryFilters.Any())
                return Enumerable.Empty<PrimaryFilterCategoryModel>();

            var filterCategoryModel = new List<PrimaryFilterCategoryModel>();
            foreach (var filter in primaryFilters)
            {
                var primaryFilterNode = UmbracoContext.Content.GetByRoute($"/{filter.ReplaceEmptySpaceByDash()}/");
                var filterGroups = primaryFilterNode.Children;

                var primaryFilter = new PrimaryFilterCategoryModel
                {
                    Key = primaryFilterNode.Name,
                    Name = primaryFilterNode.GetProperty("primaryFilterName").Value().ToString(),
                    Items = filterGroups.Select(x => new FilterCategoryModel
                    {
                        Key = x.Name,
                        Name = x.GetProperty("groupName").Value().ToString(),
                        IsPrimaryFilter = true,
                        ChildGroupName = x.GetProperty("monthTitle").Value().ToString(),
                        Items = x.Children.Select(filter => new FilterItemModel
                        {
                            Key = $"{x.Name}:{filter.Id}",
                            Name = filter.GetProperty("filterName").Value().ToString(),
                            IsPrimarySubFilter = true,
                            PrimaryFilterKey = x.Name
                        }).ToList()
                    })
                };

                filterCategoryModel.Add(primaryFilter);
            }

            return filterCategoryModel;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public IEnumerable<FilterCategoryModel> GetFilterCategoryModel([FromUri] IEnumerable<string> filterBy, [FromUri] string activePrimaryFilterCategory, [FromUri] string groupByFilter)
        {
            return filterBy.Any() ? GetFilterCategoryModels(filterBy, activePrimaryFilterCategory) : Enumerable.Empty<FilterCategoryModel>();
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public IEnumerable<FilterCategoryModel> GetFilterCategoryModelStatics([FromBody] SearchRequest searchRequest, [FromUri] bool isGrandParent, [FromUri] string activePrimaryFilterCategory)
        {
            var filterCategoryModel = GetFilterCategoryModels(searchRequest.FilterBy, activePrimaryFilterCategory);
            return GetFilterCategoryModelWithItemCountBySearchRequest(filterCategoryModel, searchRequest);
        }

        private IEnumerable<FilterCategoryModel> GetFilterCategoryModels(IEnumerable<string> filterBy, string activePrimaryFilterCategory)
        {
            var filterCategoryModels = _filterCategoryService.GetFilterCategories(filterBy).ToList();

            if (!string.IsNullOrEmpty(activePrimaryFilterCategory))
            {
                filterCategoryModels.Add(GetSubFilterGroupByRoute(activePrimaryFilterCategory));
            }

            return filterCategoryModels;
        }

        private IEnumerable<FilterCategoryModel> GetFilterCategoryModelWithItemCountBySearchRequest(IEnumerable<FilterCategoryModel> filterCategoryModels, SearchRequest searchRequest)
        {
            foreach (var filterCategoryModel in filterCategoryModels)
            {
                var filterCategoryModelItems = filterCategoryModel.Items.ToList();
                foreach (var filterItemModel in filterCategoryModelItems)
                {
                    var initialSearchRequest = GetSearchRequestForCount(searchRequest, filterCategoryModel, filterItemModel);
                    var searchRequestModel = _mapper.Map<SearchRequestModel>(initialSearchRequest);

                    filterItemModel.ItemsCount = _listingComponentService.GetTotalItemsCountBySearchRequestModel(IndexConstants.ContentIndex, searchRequestModel);
                }

                filterCategoryModel.Items = filterCategoryModelItems.Where(x => x.ItemsCount > 0);
            }

            return filterCategoryModels.Where(x => x.Items.Any()).ToList();
        }

        private SearchRequest GetSearchRequestForCount(SearchRequest searchRequest, FilterCategoryModel filterCategoryModel, FilterItemModel filterItemModel)
        {
            var searchRequestForCount = new SearchRequest();

            var criteria = searchRequest.Criteria.Where(x => x.FilterCategoryKey != filterCategoryModel.Key).ToList();

            criteria.Add(new FilterCriterion
            {
                FilterCategoryKey = filterCategoryModel.Key,
                FilterValue = filterItemModel.Name,
                IsPrimarySubFilter = filterItemModel.IsPrimarySubFilter,
                PrimaryFilterKey = filterItemModel.PrimaryFilterKey
            });

            if (!string.IsNullOrEmpty(searchRequest.GroupByFilter))
            {
                var filterCategoryModels = _filterCategoryService.GetFilterCategories(new[] {searchRequest.GroupByFilter});
                foreach (var groupFilter in filterCategoryModels)
                {
                    foreach (var groupFilterItem in groupFilter.Items)
                    {
                        criteria.Add(new FilterCriterion
                        {
                            FilterCategoryKey = groupFilter.Key,
                            FilterValue = groupFilterItem.Name,
                            IsPrimarySubFilter = filterItemModel.IsPrimarySubFilter,
                            PrimaryFilterKey = filterItemModel.PrimaryFilterKey
                        });
                    }
                }
            }

            searchRequestForCount.Paths = searchRequest.Paths ?? Enumerable.Empty<string>();
            searchRequestForCount.IncludedContentIds = searchRequest.IncludedContentIds ?? Enumerable.Empty<string>();
            searchRequestForCount.Criteria = criteria;
            searchRequestForCount.DefaultCriteria = searchRequest.DefaultCriteria;

            return searchRequestForCount;
        }

        private FilterCategoryModel GetSubFilterGroupByRoute(string filterGroupRoute)
        {
            var filterGroupNode = UmbracoContext.Content.GetByRoute($"/{filterGroupRoute.ReplaceEmptySpaceByDash()}/");
            var filters = filterGroupNode.Children;

            return new FilterCategoryModel
            {
                Name = filterGroupNode.GetProperty("groupName").Value().ToString(),
                Key = filterGroupNode.Name,
                IsPrimaryFilter = true,
                ChildGroupName = filterGroupNode.GetProperty("monthTitle").Value().ToString(),
                Items = filters.Select(x => new FilterItemModel
                {
                    Key = $"{filterGroupNode.Name}:{x.Id}",
                    Name = x.GetProperty("filterName").Value().ToString(),
                    IsPrimarySubFilter = true,
                    PrimaryFilterKey = filterGroupNode.Name
                })
            };
        }
    }
}