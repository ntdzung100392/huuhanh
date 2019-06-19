using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductCategoryViewModel
    {
        public IEnumerable<FilterCategoryModel> FilterCategories { get; set; } = Enumerable.Empty<FilterCategoryModel>();
    }
}