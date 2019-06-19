using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMapper _mapper;

        public MenuItemService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public MenuItemViewModel GetViewModel(IEnumerable<MenuItem> menuItems)
        {
            var menuItemModels = _mapper.Map<IEnumerable<MenuItemModel>>(menuItems).ToList();

            return new MenuItemViewModel
            {
                MenuItems = menuItemModels
            };
        }

        public IEnumerable<IconLinkModel> GetIconLinks(IEnumerable<IconLink> topNavigationIcons)
        {
            var topIconLinksViewModel = _mapper.Map<IEnumerable<IconLinkModel>>(topNavigationIcons).ToList();

            foreach (var iconLinkModel in topIconLinksViewModel)
            {
                iconLinkModel.IconCssClass = Icon.FromDisplayName(iconLinkModel.Icon)?.Value;
            }

            return topIconLinksViewModel;
        }
    }

    public interface IMenuItemService
    {
        MenuItemViewModel GetViewModel(IEnumerable<MenuItem> menuItems);
        IEnumerable<IconLinkModel> GetIconLinks(IEnumerable<IconLink> topNavigationIcons);
    }
}