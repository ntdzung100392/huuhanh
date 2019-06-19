using System.Collections.Generic;

namespace HHCoApps.CMSWeb.Services.Models
{
    public class Step
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string BackgroundImageUrl { get; set; }
        public Dictionary<string, string> MappedConditions { get; set; } = new Dictionary<string, string>();
        public BaseBlock[] Blocks { get; set; } = new BaseBlock[0];
    }
}