using System;
using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models.RequestModels
{
    public class SearchRequest
    {
        public IEnumerable<string> FilterBy { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> IncludedContentIds { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> Paths { get; set; } = Enumerable.Empty<string>();
        public string SearchText { get; set; }
        public IEnumerable<FilterCriterion> Criteria { get; set; } = Enumerable.Empty<FilterCriterion>();
        public IEnumerable<FilterCriterion> DefaultCriteria { get; set; } = Enumerable.Empty<FilterCriterion>();
        public string GroupByFilter { get; set; }
        public SortingRequest SortingRequest { get; set; }

        public SearchRequest Clone()
        {
            return new SearchRequest
            {
                Criteria = Criteria.ToArray(),
                DefaultCriteria = DefaultCriteria.ToArray(),
                SortingRequest = SortingRequest
            };
        }
    }
}