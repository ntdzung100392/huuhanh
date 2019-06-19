using HHCoApps.CMSWeb.Models.Enums;

namespace HHCoApps.CMSWeb.Models
{
    public class BannerComponentViewModel
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public string BackgroundColor { get; set; }
        public string TitleColor { get; set; }
        public string LeftImageUrl { get; set; }
        public string LeftImageTitle { get; set; }
        public string LeftUrl { get; set; }
        public string RightImageUrl { get; set; }
        public string RightImageTitle { get; set; }
        public string RightUrl { get; set; }
    }
}