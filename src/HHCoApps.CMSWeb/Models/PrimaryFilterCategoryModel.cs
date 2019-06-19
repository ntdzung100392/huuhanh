using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class PrimaryFilterCategoryModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<FilterCategoryModel> Items { get; set; } = Enumerable.Empty<FilterCategoryModel>();
    }
}