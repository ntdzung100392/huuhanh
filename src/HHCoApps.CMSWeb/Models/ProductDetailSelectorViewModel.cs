using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductDetailSelectorViewModel
    {
        public string Title { get; set; }
        public IEnumerable<string> Sizes { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<ColorModel> Colors { get; set; } = Enumerable.Empty<ColorModel>();
        public IEnumerable<ShopifyMapping> ShopifyMappings { get; set; }
        public string TimberCaption { get; set; }
        public string ColorCaption { get; set; }
    }

    public class ColorModel
    {
        public string ColorName { get; set; }
        public string ColorImageUrl { get; set; }
        public string ColorUid { get; set; }
        public IEnumerable<TimberModel> Timbers { get; set; } = Enumerable.Empty<TimberModel>();
    }

    public class TimberModel
    {
        public string TimberName { get; set; }
        public string TimberUid { get; set; }
        public IEnumerable<CoatModel> Coats { get; set; } = Enumerable.Empty<CoatModel>();
    }

    public class CoatModel
    {
        public string CoatOption { get; set; }
        public string CoatImageUrl { get; set; }
    }
    public class ShopifyMapping
    {
        public string SizeColor { get; set; }
        public string SKU { get; set; }
    }
}