using System.Collections.Generic;

namespace HHCoApps.CMSWeb.Models
{
    public class GeoJson
    {
        public string Type;
        public List<StoreInformation> Features;
    }

    public class StoreInformation
    {
        public GeoMetry Geometry;
        public string Type;
        public Properties Properties;
    }

    public class GeoMetry
    {
        public string Type;
        public double[] Coordinates;
    }

    public class Properties
    {
        public string Category;
        public string StoreId;
        public string Name;
        public string Website;
        public double DistanceFromOrigin;
        public Address Address;
        public string StoreUrl;
    }

    public class Address
    {
        public string Suburb;
        public string PostCode;
        public string Phone;
        public string FormattedAddress;
        public string Fax;
    }
}