using HHCoApps.CMSWeb.Headspring;
using Umbraco.Core;

namespace HHCoApps.CMSWeb.Models.Enums
{
    public class FilterCategory : Enumeration<FilterCategory, string>
    {
        public static FilterCategory AvailableSizes = new FilterCategory("Available Sizes", "Available Sizes") { PropertyAlias = "availableSizes", DataTypeName = "Available Sizes - Dropdown" };
        public static FilterCategory ProductSubCategory = new FilterCategory("Product Sub-category", "Product Sub-category") { PropertyAlias = "productSubCategory", DataTypeName = "Product Sub-category - Dropdown" };
        public static FilterCategory BlogCategory = new FilterCategory("Blog Category", "Blog Category") { PropertyAlias = "blogCategory", DataTypeName = "Blog Category - Dropdown" };
        public FilterCategory(string value, string displayName) : base(value, displayName)
        {
        }

        public string PropertyAlias { get; set; }
        public string SearchablePropertyAlias => "searchable" + PropertyAlias.ToFirstUpper();
        public string DataTypeName { get; set; }
    }
}