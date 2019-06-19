using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HHCoApps.CMSWeb.Services.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class RootStep : Step
    {
        public Step[] Steps { get; set; }
    }
}