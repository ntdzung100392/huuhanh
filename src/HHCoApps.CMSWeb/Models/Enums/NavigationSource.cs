using HHCoApps.CMSWeb.Headspring;

namespace HHCoApps.CMSWeb.Models.Enums
{
    public class NavigationSource : Enumeration<NavigationSource, string>
    {
        public static NavigationSource ParentPage = new NavigationSource("ParentPage", "Parent Page");
        public static NavigationSource GrandParentPage = new NavigationSource("GrandParentPage", "Grand Parent Page");

        public static NavigationSource Level1 = new NavigationSource("Level1", "Level 1");
        public static NavigationSource Level2 = new NavigationSource("Level2", "Level 2");

        public NavigationSource(string value, string displayName) : base(value, displayName)
        {
        }
    }
}