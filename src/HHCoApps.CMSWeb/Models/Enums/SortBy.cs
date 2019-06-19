using HHCoApps.CMSWeb.Headspring;

namespace HHCoApps.CMSWeb.Models.Enums
{
    public class SortBy : Enumeration<SortBy, string>
    {
        public static SortBy CreatedDate = new SortBy("createDate", "Created Date");
        public static SortBy LastModifiedDate = new SortBy("updateDate", "Last Modified Date");
        public static SortBy Title = new SortBy("pageTitle", "Title");
        public static SortBy SortOrder = new SortBy("sortOrder", "Sort Order");

        public SortBy(string value, string displayName) : base(value, displayName)
        {
        }
    }
}