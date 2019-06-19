using HHCoApps.CMSWeb.Headspring;

namespace HHCoApps.CMSWeb.Models.Enums
{
    public class ContentSource : Enumeration<ContentSource, string>
    {
        public static ContentSource FromFixedItems = new ContentSource("FromFixedItems", "From Fixed Items");
        public static ContentSource FromChildPages = new ContentSource("FromChildPages", "From Child Pages");
        public static ContentSource FromDefaultList = new ContentSource("FromDefaultList", "From Default List");

        public ContentSource(string value, string displayName) : base(value, displayName)
        {
        }
    }
}