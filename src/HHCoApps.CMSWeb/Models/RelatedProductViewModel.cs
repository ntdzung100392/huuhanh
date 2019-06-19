using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class RelatedProductViewModel
    {
        public IEnumerable<RelatedProductModel> RelatedProducts { get; set; } = Enumerable.Empty<RelatedProductModel>();

    }
}