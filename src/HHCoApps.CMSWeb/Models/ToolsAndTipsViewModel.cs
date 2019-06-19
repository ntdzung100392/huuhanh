using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class ToolsAndTipsViewModel
    {
        public IEnumerable<IconLinkModel> IconLinks { get; set; } = Enumerable.Empty<IconLinkModel>();
    }
}