using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Models;

namespace HHCoApps.CMSWeb.Services.Models
{
    public class GalleryComponentViewModel
    {
        public string Title { get; set; }
        public string ViewMoreLabel { get; set; }
        public string ViewMoreUrl { get; set; }
        public IEnumerable<IEnumerable<ContentInfoModel>> Rows { get; set; } = Enumerable.Empty<IEnumerable<ContentInfoModel>>();
        public Guid UId { get; set; }
        public string ViewMoreTarget { get; set; }
    }
}