using System;
using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    [Obsolete("Use ItemListingService instead of creating new")]
    public class ItemListingVideoViewModel
    {
        public IEnumerable<LinkItemModel> ContentInfos { get; set; } = Enumerable.Empty<LinkItemModel>();
        public string Label { get; set; } = string.Empty;
        public bool DisplayLabel => !string.IsNullOrEmpty(Label) && ContentInfos.Any();
        public int NumberOfDisplayItems { get; set; }
        public string StarterNodeUrl { get; set; }
        public string ViewMoreLabel { get; set; }
        public bool DisplayGoToStarterNode => !string.IsNullOrEmpty(StarterNodeUrl) && ContentInfos.Any();
    }
}