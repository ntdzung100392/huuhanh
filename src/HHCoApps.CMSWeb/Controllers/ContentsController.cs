using System;
using AutoMapper;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services;
using HHCoApps.CMSWeb.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Helpers.Enum;
using HHCoApps.CMSWeb.Models.Enums;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class ContentsController : UmbracoApiController
    {
        const int DefaultPageSize = 9;
        const int DefaultPageSizeFromGrandParent = 9;

        private readonly IMapper _mapper;
        private readonly IContentIndexQueryService _queryService;
        private readonly IListingComponentService _listingComponentService;
        private readonly IFilterCategoryService _filterCategoryService;

        public ContentsController(IMapper mapper, IContentIndexQueryService queryService, IListingComponentService listingComponentService, IFilterCategoryService filterCategoryService)
        {
            _mapper = mapper;
            _queryService = queryService;
            _listingComponentService = listingComponentService;
            _filterCategoryService = filterCategoryService;
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public PagingResult<ContentInfoModel> GetProducts([FromBody] SearchRequest searchRequest, [FromUri]int pageNumber = 1, [FromUri]int pageSize = DefaultPageSize)
        {
            if (!searchRequest.Paths.Any())
                return new PagingResult<ContentInfoModel>();

            var searchRequestModel = _mapper.Map<SearchRequestModel>(searchRequest);
            var searchResults = _queryService.GetPagingResult<ContentInfoModel>(
                IndexConstants.ContentIndex, new[] { IndexConstants.ProductNodeTypeAlias }, searchRequestModel, pageNumber, pageSize);

            SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.ThreeColumnSlider);

            return searchResults;
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public PagingResult<ContentInfoModel> GetOthers([FromBody] SearchRequest searchRequest, [FromUri]int pageNumber = 1, [FromUri]int pageSize = DefaultPageSize)
        {
            if (!searchRequest.Paths.Any())
                return new PagingResult<ContentInfoModel>();

            var searchRequestModel = _mapper.Map<SearchRequestModel>(searchRequest);
            var searchResults = _queryService.GetPagingResult<ContentInfoModel>(
                IndexConstants.ContentIndex, IndexConstants.AllNodeTypeAliases, searchRequestModel, pageNumber, pageSize);

            SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.ThreeColumnBlog);

            return searchResults;
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public PagingResult<ContentInfoModel> GetArticles([FromBody] SearchRequest searchRequest, [FromUri]int pageNumber = 1, [FromUri]int pageSize = DefaultPageSize)
        {
            if (!searchRequest.Paths.Any())
                return new PagingResult<ContentInfoModel>();

            var searchRequestModel = _mapper.Map<SearchRequestModel>(searchRequest);
            var searchResults = _queryService.GetPagingResult<ContentInfoModel>(
                IndexConstants.ContentIndex, new[] { IndexConstants.ArticleNodeTypeAlias }, searchRequestModel, pageNumber, pageSize);

            SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.ThreeColumnBlog);

            return searchResults;
        }

        [HttpPost]
        public PagingResult<ContentInfoModel> SearchContents([FromBody] SearchRequest searchRequest, [FromUri]string imageCropProfileName, [FromUri]string contentType, [FromUri]int pageNumber = 1, [FromUri]int pageSize = DefaultPageSize)
        {
            _filterCategoryService.UpdateSearchRequestCriteriaFilterKey(searchRequest.Criteria);

            var searchRequestModel = _mapper.Map<SearchRequestModel>(searchRequest);
            if (searchRequestModel.IncludedContentIds.Any())
            {
                searchRequestModel.IncludedContentIds = _listingComponentService.GetChildNodesIdsByIds(searchRequest.IncludedContentIds.Where(x => !string.IsNullOrEmpty(x)), searchRequestModel);
            }

            var searchResults = _queryService.GetContentsBySearchRequestModel<ContentInfoModel>(IndexConstants.ContentIndex, searchRequestModel, contentType, pageNumber, pageSize);

            if (Enum.TryParse(imageCropProfileName, out ReactLayoutConstant enumCropProfile))
            {
                switch (enumCropProfile)
                {
                    case ReactLayoutConstant.TwoColumn:
                        SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.TwoColumn);
                        break;
                    case ReactLayoutConstant.ThreeColumnsWithFlexibleHeightItems:
                        SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.ThreeColumnsWithFlexibleHeightItems);
                        break;
                    case ReactLayoutConstant.FourColumns:
                        SetImageUrlCropModeForPagingResult(searchResults, ImageCropProfile.FourColumn);
                        break;
                }
            }

            return searchResults;
        }

        [HttpPost]
        public IEnumerable<ContentInfoGroupModel> SearchContentsByFiltersAndGrandParentId([FromBody] SearchRequest searchRequest, [FromUri]string imageCropProfileName, [FromUri]string contentType, [FromUri]int pageSize = DefaultPageSizeFromGrandParent)
        {
            var searchResults = _listingComponentService.GetContentInfoGroupedBySearchRequestAndGrandParentId(IndexConstants.ContentIndex, searchRequest, contentType, pageSize);

            if (Enum.TryParse(imageCropProfileName, out ReactLayoutConstant enumCropProfile))
            {
                switch (enumCropProfile)
                {
                    case ReactLayoutConstant.TwoColumn:
                        SetImageUrlCropModeForContentInfoGroupModel(searchResults, ImageCropProfile.TwoColumn);
                        break;
                    case ReactLayoutConstant.FourColumns:
                        SetImageUrlCropModeForContentInfoGroupModel(searchResults, ImageCropProfile.FourColumn);
                        break;
                }
            }

            return searchResults;
        }

        private void SetImageUrlCropModeForPagingResult(PagingResult<ContentInfoModel> searchResult, ImageCropProfile cropProfile)
        {
            foreach (var item in searchResult.Items)
            {
                item.ImageUrl = item.ImageUrl.GetCropImageUrl(item.ImageHeight, item.ImageWidth, cropProfile);
            }
        }

        private void SetImageUrlCropModeForContentInfoGroupModel(IEnumerable<ContentInfoGroupModel> searchResults, ImageCropProfile cropProfile)
        {
            if (searchResults.Any())
            {
                foreach (var searchResult in searchResults)
                {
                    foreach (var item in searchResult.ChildItems)
                    {
                        item.ImageUrl = item.ImageUrl.GetCropImageUrl(item.ImageHeight, item.ImageWidth, cropProfile);
                    }
                }
            }
        }
    }
}