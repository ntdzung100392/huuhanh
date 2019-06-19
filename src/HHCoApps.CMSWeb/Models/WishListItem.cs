using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class WishListItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ColorName { get; set; }
        public string ColorImageUrl { get; set; }
        public string ProductUri { get; set; }
        public string Size { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductImageAlt { get; set; }
        public string Caption { get; set; }
        public int Quantity { get; set; }
    }
}