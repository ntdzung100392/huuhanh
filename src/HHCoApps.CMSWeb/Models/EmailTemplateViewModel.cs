using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class EmailTemplateViewModel
    {
        public string EmailSubject { get; set; }
        public string FromEmail { get; set; }
        public string FromContactName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string HeaderBannerImage { get; set; }
        public string HeaderBannerLogo { get; set; }
        public string FooterBannerImage { get; set; }
        public string HeaderDescription { get; set; }
        public string FindYourStockistDescription { get; set; }
        public string FindYourStockistLink { get; set; }
        public IEnumerable<WishListItem> WishListItems { get; set; } = Enumerable.Empty<WishListItem>();
        public IEnumerable<SocialLink> SocialNetworks { get; set; } = Enumerable.Empty<SocialLink>();
    }
   
}