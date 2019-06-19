using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class FooterNavigationModel
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<FooterNavigationModel> ChildNodes { get; set; } = Enumerable.Empty<FooterNavigationModel>();
    }
}