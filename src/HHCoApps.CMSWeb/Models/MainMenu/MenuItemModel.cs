using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.Models;

namespace HHCoApps.CMSWeb.Models
{
    public class MenuItemModel
    {
        public string Key { get; set; }
        public Link Link { get; set; }
        public string Title { get; set; }
        public int NumberOfChildItemsPerColumn { get; set; }
        public IEnumerable<MenuItemModel> ChildItems { get; set; } = Enumerable.Empty<MenuItemModel>();
        public bool HasChildItems { get; set; }
    }
}