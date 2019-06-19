using System;
using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Services.Models
{
    public class SearchRequestModel
    {
        public IEnumerable<string> IncludedContentIds { get; set; } = Enumerable.Empty<string>();
        [Obsolete("Should be removed when removed in SearchRequest")]
        public string Path { get; set; }
        public IEnumerable<string> Paths { get; set; } = Enumerable.Empty<string>();
        public string SearchText { get; set; }
        public IEnumerable<FilterCriterionModel> Criteria { get; set; } = Enumerable.Empty<FilterCriterionModel>();
        public IEnumerable<FilterCriterionModel> DefaultCriteria { get; set; } = Enumerable.Empty<FilterCriterionModel>();
        public SortingModel SortingModel { get; set; }
        public string NodeTypeAlias { get; set; }
        public string GroupByFilter { get; set; }

        public SearchRequestModel Clone()
        {
            return new SearchRequestModel
            {
                Criteria = Criteria.ToArray(),
                DefaultCriteria = DefaultCriteria.ToArray(),
                SortingModel = SortingModel
            };
        }
    }
}