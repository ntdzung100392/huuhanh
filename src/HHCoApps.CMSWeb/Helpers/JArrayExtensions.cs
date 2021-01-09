using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class JArrayExtensions
    {
        public static void AddShopifyMapping(this JArray jArray, string size, string color, string sku)
        {
            var nestedContentItem = new JObject();
            nestedContentItem.Add("key", Guid.NewGuid());
            nestedContentItem.Add("name", $"[\"{size}\"] {color} : {sku}");
            nestedContentItem.Add("ncContentTypeAlias", "productVariantMapping");
            nestedContentItem.Add(nameof(ProductVariantMapping.Sizes).ToLower(), JArray.Parse($"['{size}']"));
            nestedContentItem.Add(nameof(ProductVariantMapping.Colors).ToLower(), color);
            nestedContentItem.Add("shopifyValue", sku);
            jArray.Add(nestedContentItem);
        }
    }
}