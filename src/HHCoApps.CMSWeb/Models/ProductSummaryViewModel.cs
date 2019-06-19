using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Umbraco.Web.Models;

namespace HHCoApps.CMSWeb.Models
{
    public class ProductSummaryViewModel : ContentInfoModel
    {
        public IEnumerable<ImageModel> Images { get; set; } = Enumerable.Empty<ImageModel>();
        public IEnumerable<Link> DownloadLinks { get; set; } = Enumerable.Empty<Link>();
        public bool AddToWishListEnabled { get; set; }
        public bool FindAStockistEnabled { get; set; }
        public JToken Summary { get; set; }
    }
}