using System;
using Our.Umbraco.GMaps.Models;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class GmapExtensions
    {
        public static string GetFormattedAddress(this GmapsAddress address)
        {
            if (address == null)
                return string.Empty;

            return $"{address.FullAddress} {address.City} {address.Country} {address.PostalCode}";
        }
    }
}