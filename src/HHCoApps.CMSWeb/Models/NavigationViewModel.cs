using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class NavigationViewModel
    {
        public string Title { get; set; }
        public IEnumerable<NavigationModel> NavigationNodes { get; set; } = Enumerable.Empty<NavigationModel>();
        public IEnumerable<LinkItemModel> BreadcrumbNodes { get; set; } = Enumerable.Empty<LinkItemModel>();
    }
}