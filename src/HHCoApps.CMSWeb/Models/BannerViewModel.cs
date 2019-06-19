using HHCoApps.CMSWeb.Models.Enums;

namespace HHCoApps.CMSWeb.Models
{
    public class BannerViewModel
    {
        public string Label { get; set; }
        public string DescriptionHtml { get; set; }
        public string ImageUrl { get; set; }
        public string NavigationUrl { get; set; }
        public string NavigationTitle { get; set; }
        public Position ImagePosition { get; set; }
        public bool DisplayNavigationLink => !string.IsNullOrEmpty(NavigationTitle);
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string PageTitle { get; set; }
        public string ParentPageTitle { get; set; }
        public string SubTitle { get; set; }
        public string BrandImageUrl { get; set; }
    }
}