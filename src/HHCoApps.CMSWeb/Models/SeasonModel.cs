using System.Collections.Generic;
using Umbraco.Web.Models;

namespace HHCoApps.CMSWeb.Models
{
    public class SeasonModel
    {
        public string Title { get; set; }
        public Link SeasonLink { get; set; }
        public IEnumerable<string> Months { get; set; }
    }
}