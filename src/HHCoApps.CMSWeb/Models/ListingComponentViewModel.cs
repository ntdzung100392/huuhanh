using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Services.Models;
using HHCoApps.CMSWeb.Models.RequestModels;

namespace HHCoApps.CMSWeb.Models
{
    public class ListingComponentViewModel
    {
        public int StarterNodeId { get; set; }
        public string ContentType { get; set; }
        public bool IsGroupBySubCategory { get; set; }
        public int NumberItemsPerPage { get; set; }
        public string TemplateName { get; set; }
        public IEnumerable<string> FilteringBy { get; set; }
        public bool IsEnabledFilter { get; set; }
        public IEnumerable<int> IncludedContentIds { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<string> PrimaryFilterIds { get; set; }
        public IEnumerable<FilterCriterionModel> DefaultFilteringBy { get; set; } = Enumerable.Empty<FilterCriterionModel>();
        public SortingRequest SortingRequest { get; set; }
        public string GroupByFilter { get; set; }
        public string ViewMoreLabel { get; set; }
        public string ViewDetailsLabel { get; set; }
    }
}