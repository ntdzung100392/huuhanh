using HHCoApps.CMSWeb.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Models
{
    public class ItemListingSource
    {
        public IEnumerable<ContentInfoModel> ContentInfos { get; set; } = Enumerable.Empty<ContentInfoModel>();
        public IEnumerable<FilterTypeValueOptions> DefaultFilters { get; set; } = Enumerable.Empty<FilterTypeValueOptions>();
        public IPublishedContent StarterNode { get; set; }
        public string ContentSourceOption { get; set; }
        public string OrderType { get; set; }
        public string SortBy { get; set; }
        public int NumberOfDisplayItems { get; set; }
    }
}