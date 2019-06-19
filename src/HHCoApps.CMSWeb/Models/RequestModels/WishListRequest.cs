using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models.RequestModels
{
    public class WishListEmailRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ReCaptchaToken { get; set; }
        public IEnumerable<WishListItem> wishListItems { get; set; } = Enumerable.Empty<WishListItem>();
    }
}