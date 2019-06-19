using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Services.Models.ProductSelector
{
    public class ValidateMappingResponseModel
    {
        public bool IsMappingValid { get; set; }
        public IEnumerable<string> ValidationMessages { get; set; }
    }
}