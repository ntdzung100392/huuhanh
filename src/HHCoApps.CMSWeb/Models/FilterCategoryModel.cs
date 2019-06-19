using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class FilterCategoryModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsPrimaryFilter { get; set; }
        public string ChildGroupName { get; set; }
        public IEnumerable<FilterItemModel> Items { get; set; } = Enumerable.Empty<FilterItemModel>();
    }
}