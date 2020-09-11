using AutoMapper;
using HHCoApps.CMSWeb.Composers.Indexing;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Caching;
using Examine;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Macros;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class ItemListingService : IItemListingService
    {
        private readonly IContentIndexQueryService _contentIndexQueryService;
        private readonly IMapper _mapper;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly int _defaultNumberOfDisplayItems = 4;

        private readonly bool _isBackOfficeRequest;

        public ItemListingService(IDataTypeService dataTypeService, IContentIndexQueryService contentIndexQueryService, IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
            _contentIndexQueryService = contentIndexQueryService;
            _isBackOfficeRequest = Umbraco.Web.Composing.Current.UmbracoContext.HttpContext.Request.Path.StartsWith("/umbraco/backoffice");
        }

        public ItemListingViewModel GetViewModel(ItemListing itemListing)
        {
            var viewModel = _mapper.Map<ItemListingViewModel>(itemListing);

            var numberOfDisplayItems = itemListing.NumberOfDisplayItems == default ? _defaultNumberOfDisplayItems : itemListing.NumberOfDisplayItems;

            var itemListingSource = GetItemListingSource(itemListing);

            var contentInfos = GetSortedAndOrderedContentInfos(itemListingSource, numberOfDisplayItems);

            var cmsContents = contentInfos.Where(x => !string.IsNullOrEmpty(x.Udi)).ToList();

            var nodesContents = _umbracoHelper.Content(cmsContents.Select(x => Udi.Parse(x.Udi)).ToArray());

            foreach (var content in contentInfos)
            {
                if (!string.IsNullOrEmpty(content.Udi))
                {
                    var node = nodesContents.Where(x => x.GetUDI() == content.Udi);
                    if (node.Any())
                    {
                        var nodeContentInfoModel = GetNodeContentInfo(node.First());
                        content.Title = string.IsNullOrEmpty(content.Title) ? nodeContentInfoModel.Title : content.Title;
                        content.ImageUrl = content.ImageUrl ?? nodeContentInfoModel.ImageUrl;
                        content.ImageAlt ??= nodeContentInfoModel.Title;
                        content.Caption = string.IsNullOrEmpty(content.Caption) ? nodeContentInfoModel.Caption : content.Caption;
                    }
                }
            }

            viewModel.ContentInfos = contentInfos;
            viewModel.DisplayViewMoreAsButton = itemListing.DisplayViewMoreAsButton;
            viewModel.IsBackOfficeRequest = _isBackOfficeRequest;

            return viewModel;
        }

        private ContentInfoModel GetNodeContentInfo(IPublishedContent nodeContent)
        {
            if (nodeContent == null)
                return new ContentInfoModel();

            if (nodeContent is Home)
            {
                var content = (NavigationManagement)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.NavigationManagementContent);
                return new ContentInfoModel
                {
                    Title = content?.SiteName ?? string.Empty,
                    ImageUrl = content.Logo?.Url(mode: UrlMode.Absolute) ?? string.Empty,
                    ImageAlt = content?.SiteName ?? string.Empty,
                    Caption = content?.SiteDescription ?? string.Empty
                };
            }

            if (nodeContent is IContentBase contentInfo)
            {
                var title = string.IsNullOrEmpty(contentInfo.ListingTitle) ? contentInfo.PageTitle : contentInfo.ListingTitle;
                return new ContentInfoModel
                {
                    Title = title ?? string.Empty,
                    ImageUrl = contentInfo.Images?.Select(x => x).FirstOrDefault()?.MediaUrl() ?? string.Empty,
                    ImageAlt = contentInfo.PageTitle ?? string.Empty,
                    Caption = contentInfo.Description ?? string.Empty
                };
            }

            return new ContentInfoModel();
        }

        private IEnumerable<ContentInfoModel> GetSortedAndOrderedContentInfos(ItemListingSource itemListingSource, int numberOfDisplayItems)
        {
            if (itemListingSource == null)
                return Enumerable.Empty<ContentInfoModel>();

            if (!string.IsNullOrEmpty(itemListingSource.ContentSourceOption) && itemListingSource.ContentSourceOption.Equals(ContentSource.FromChildPages.DisplayName))
                return GetContentInfoModelsFromChildNode(itemListingSource, numberOfDisplayItems);

            return GetContentInfoModelsFromItemsGroup(itemListingSource, numberOfDisplayItems);
        }

        public ItemListingSource GetItemListingSource(ItemListing itemListing)
        {
            if (!string.IsNullOrEmpty(itemListing.ContentSourceOption) && itemListing.ContentSourceOption.Equals(ContentSource.FromDefaultList.DisplayName))
                return _mapper.Map<ItemListingSource>((ListingConfiguration)itemListing.DefaultList);

            return _mapper.Map<ItemListingSource>(itemListing);
        }

        private IEnumerable<ContentInfoModel> GetContentInfoModelsFromItemsGroup(ItemListingSource itemListingSource, int numberOfDisplayItems)
        {
            return _mapper.Map<IEnumerable<ContentInfoModel>>(itemListingSource.ContentInfos.Take(numberOfDisplayItems));
        }

        private IEnumerable<ContentInfoModel> GetContentInfoModelsFromChildNode(ItemListingSource itemListingSource, int numberOfDisplayItems)
        {
            if (itemListingSource.StarterNode == null)
                return Enumerable.Empty<ContentInfoModel>();

            var filteredContentItemIds = GetFilteredContentInfos(itemListingSource);
            if (!filteredContentItemIds.Any())
            {
                filteredContentItemIds = GetContentsByStarterNodeAndCriteria(itemListingSource, Enumerable.Empty<FilterCriterionModel>()).Select(x => x.Id);
            }

            var contents = _umbracoHelper.Content(filteredContentItemIds);

            var contentInfoModels = Enumerable.Empty<ContentInfoModel>();

            var sortBy = string.IsNullOrEmpty(itemListingSource.SortBy) ? SortBy.SortOrder : SortBy.FromDisplayName(itemListingSource.SortBy);
            if (!string.IsNullOrEmpty(itemListingSource.OrderType) || !sortBy.Equals(SortBy.SortOrder))
            {
                var orderBy = OrderType.FromDisplayName(itemListingSource.OrderType);

                if (orderBy.Equals(OrderType.Descending))
                {
                    if (sortBy.Equals(SortBy.CreatedDate))
                    {
                        contents = contents.OrderByDescending(x => x.CreateDate);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                    else if (sortBy.Equals(SortBy.LastModifiedDate))
                    {
                        contents = contents.OrderByDescending(x => x.UpdateDate).ThenByDescending(x => x.CreateDate);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                    else if (sortBy.Equals(SortBy.Title))
                    {
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                        contentInfoModels = contentInfoModels.OrderByDescending(x => x.Title);
                    }
                    else
                    {
                        contents = contents.OrderByDescending(x => x.SortOrder);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                }
                else
                {
                    if (sortBy.Equals(SortBy.CreatedDate))
                    {
                        contents = contents.OrderBy(x => x.CreateDate);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                    else if (sortBy.Equals(SortBy.LastModifiedDate))
                    {
                        contents = contents.OrderBy(x => x.UpdateDate).ThenBy(x => x.CreateDate);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                    else if (sortBy.Equals(SortBy.Title))
                    {
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                        contentInfoModels = contentInfoModels.OrderBy(x => x.Title);
                    }
                    else
                    {
                        contents = contents.OrderBy(x => x.SortOrder);
                        contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
                    }
                }
            }
            else
            {
                contents = contents.OrderBy(x => x.SortOrder);
                contentInfoModels = _mapper.Map<IEnumerable<ContentInfoModel>>(contents);
            }

            return contentInfoModels.Take(numberOfDisplayItems);
        }

        private IEnumerable<string> GetFilteredContentInfos(ItemListingSource itemListingSource)
        {
            if (itemListingSource.DefaultFilters == null || !itemListingSource.DefaultFilters.Any())
                return Enumerable.Empty<string>();

            var criteria = new List<FilterCriterionModel>();
            foreach (var filterTypeValueOptions in itemListingSource.DefaultFilters)
            {
                var filterCategory = FilterCategory.FromDisplayName(filterTypeValueOptions.FilterType);
                if( filterCategory == null)
                    continue;

                var filterValues = filterTypeValueOptions.FilterValues.Split(',');

                criteria.AddRange(filterValues.Select(x => new FilterCriterionModel
                {
                    FilterCategoryKey = filterCategory.Value,
                    FilterValue = x.Trim()
                }));
            }

            if (!criteria.Any())
                return Enumerable.Empty<string>();

            return GetContentsByStarterNodeAndCriteria(itemListingSource, criteria).Select(x => x.Id);
        }

        private ISearchResults GetContentsByStarterNodeAndCriteria(ItemListingSource itemListingSource, IEnumerable<FilterCriterionModel> criteria)
        {
            var searchRequestModel = new SearchRequestModel
            {
                Paths = itemListingSource.StarterNode.ContentType.Alias.Equals(IndexConstants.LandingPageNodeTypeAlias) ? itemListingSource.StarterNode.Children.Select(x => x.Path) : new[] { itemListingSource.StarterNode.Path },
                Criteria = criteria
            };

            var nodeTypeAlias = _contentIndexQueryService.GetNodeTypeAliasesByParentContentType(itemListingSource.StarterNode.ContentType.Alias);
            return _contentIndexQueryService.GetContentsBySearchRequestModel(IndexConstants.ContentIndex, new[] { nodeTypeAlias }, searchRequestModel);
        }
    }

    public interface IItemListingService
    {
        ItemListingViewModel GetViewModel(ItemListing itemListing);
    }
}