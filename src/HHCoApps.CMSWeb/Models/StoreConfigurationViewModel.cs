using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class StoreConfigurationViewModel
    {
        public string ApiKey { get; set; }
        public int MaxStoresShown { get; set; }
        public string DefaultCountryCode { get; set; }
        public string DefaultCoordinates { get; set; }
        public int DefaultZoomLevel { get; set; }
    }
}