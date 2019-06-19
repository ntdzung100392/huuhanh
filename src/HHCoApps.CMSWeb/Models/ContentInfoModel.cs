using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class ContentInfoModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ParentTitle { get; set; }
        public string ParentUrl { get; set; }
        public string NavigationTitle { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string Target { get; set; }
        public string Udi { get; set; }
        public IEnumerable<RelatedProductModel> RelatedProducts { get; set; } = Enumerable.Empty<RelatedProductModel>();
    }
}