using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Models
{
    public class HubSpotSettings
    {
        public string ClientId { get; set; }
        public string[] Scopes { get; set; } = new string[] { };
        public string RedirectUri { get; set; }
    }
}