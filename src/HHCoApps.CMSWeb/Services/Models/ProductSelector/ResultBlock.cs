using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Services.Models.ProductSelector
{
    public class ResultBlock : BaseBlock
    {
        public string Id { get; set; }
        public int ItemPerPage { get; set; }
        public string NoItemText { get; set; }
        public string ShowMoreLabel { get; set; }
    }
}