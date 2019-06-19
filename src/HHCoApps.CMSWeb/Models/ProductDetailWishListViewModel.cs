using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductDetailWishListViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductUri { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductImageAlt { get; set; }
        public string ColorName { get; set; }
        public string ColorImageUrl { get; set; }
        public string Size { get; set; }
        public bool IsValidProduct { get; set; }
    }
}