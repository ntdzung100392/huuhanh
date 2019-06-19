using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductNavigationViewModel
    {
        public string ViewAllLabel { get; set; }
        public string ViewAllLink { get; set; }
        public string ViewAllLinkTarget { get; set; }
        public List<MenuItemModel> NodeChildrens { get; set; } = new List<MenuItemModel>();
    }
}