using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.CMSWeb.Models
{
    public class ItemListingViewModel
    {
        public IEnumerable<ContentInfoModel> ContentInfos { get; set; } = Enumerable.Empty<ContentInfoModel>();
        public string Label { get; set; } = string.Empty;
        public string Caption { get; set; } = string.Empty;
        public string SubLabel { get; set; } = string.Empty;
        public bool DisplayLabel => IsBackOfficeRequest? !string.IsNullOrEmpty(Label) : !string.IsNullOrEmpty(Label) && ContentInfos.Any();
        public int NumberOfDisplayItems { get; set; }
        public string ViewMoreUrl { get; set; }
        public string ViewMoreLabel { get; set; }
        public bool DisplayViewMoreLink => !string.IsNullOrEmpty(ViewMoreUrl) && ContentInfos.Any();
        public bool DisplaySubLabel => !string.IsNullOrEmpty(SubLabel);
        public bool DisplayViewMoreAsButton { get; set; }
        public bool IsBackOfficeRequest { get; set; }
        public string BackgroundColour { get; set; }
        public string FontColour { get; set; }
    }
}