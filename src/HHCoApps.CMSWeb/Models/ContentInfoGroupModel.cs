using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class ContentInfoGroupModel : ContentInfoModel
    {
        public IEnumerable<ContentInfoModel> ChildItems { get; set; } = Enumerable.Empty<ContentInfoModel>();
        public long TotalChildItems { get; set; }
    }
}