using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HHCoApps.CMSWeb.Caching;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using Umbraco.Web.Models;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class NavigationController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMapper _mapper;

        public NavigationController(UmbracoHelper umbracoHelper, IMapper mapper)
        {
            _umbracoHelper = umbracoHelper;
            _mapper = mapper;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public List<MenuItemModel> GetProductList(string contentId)
        {
            var viewModel = new List<MenuItemModel>();
            var children = _umbracoHelper.Content(contentId).Descendants<Product>().Where(x => x.GetProperty("hideInSideNavigation").Value<bool>() == false).ToList();
            foreach (var child in children)
            {
                var menuItem = _mapper.Map<MenuItemModel>(child);
                viewModel.Add(menuItem);
            }
            return viewModel;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public ProductNavigationViewModel GetMenuItems(string contentId)
        {
            var navigationSettings = _umbracoHelper.Content<NavigationManagement>(contentId);
            var allMenus = new List<MenuItemModel>();
            var productsNodeContent = _umbracoHelper.Content<Products>(navigationSettings.ProductNavigation?.Id.ToString());
            if (productsNodeContent != null)
            {
                var children = productsNodeContent.Children.Where(x => x.GetProperty("hideInSideNavigation").Value<bool>() == false);
                if (children != null && children.Any())
                {
                    foreach (var childNode in children)
                    {
                        var menu = new MenuItemModel
                        {
                            Title = childNode.GetProperty("pageTitle").Value<string>() ?? childNode.Name,
                            Key = Guid.NewGuid().ToString(),
                            Link = new Link { Url = childNode.Url }
                        };

                        var listMenu = new List<MenuItemModel>();
                        if (childNode.Children.Any())
                        {
                            menu.HasChildItems = true;
                            var grandChildren = childNode.Children.Where(x => x.GetProperty("hideInSideNavigation").Value<bool>() == false);
                            if (grandChildren != null && grandChildren.Any())
                            {
                                foreach (var contentType in grandChildren)
                                {
                                    var submenu = _mapper.Map<MenuItemModel>(contentType);
                                    submenu.HasChildItems = contentType.Children.Any();
                                    listMenu.Add(submenu);
                                }
                            }
                        }
                        menu.ChildItems = listMenu;
                        allMenus.Add(menu);
                    }
                }
            }
            return new ProductNavigationViewModel
            {
                ViewAllLabel = navigationSettings.ViewAllLabel,
                ViewAllLinkTarget = navigationSettings.ViewAllLink?.Target ?? string.Empty,
                ViewAllLink = navigationSettings.ViewAllLink?.Url,
                NodeChildrens = allMenus
            };
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public ContentInfoModel GetProductContent(string contentId)
        {
            var content = _umbracoHelper.Content(contentId);
            var viewModel = new ContentInfoModel();
            if (content != null && content.IsDocumentType("product"))
            {
                viewModel = _mapper.Map<ContentInfoModel>(content);
            }

            return viewModel;
        }
    }
}