using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.CMSWeb.Models.Constants;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class PublishedContentQueryExtensions
    {
        public static Dictionary<string, string> GetColorDictionary(this IPublishedContentQuery publishedContentQuery)
        {
            var globalColourList = publishedContentQuery.Content(ProductConstants.GlobalColourListId) as GlobalColourList;
            var dicColour = globalColourList.Descendants<Colour>()
                                            .ToList()
                                            .ToDictionary(color => color.ColourName.ToLower(),
                                                          color => $"umb://document/{color.Key.ToString().Replace("-", "").ToLower()}");
            return dicColour;
        }

        public static Dictionary<string, Guid> GetProductDitionary(this IPublishedContentQuery publishedContentQuery)
        {
            var products = publishedContentQuery.Content(ProductConstants.ProductsNodeId) as Products;
            var dicProduct = products.Descendants<Product>()
                                   .Where(p => !p.HideInSideNavigation)
                                   .OrderBy(p => p.Name)
                                   .ToDictionary(p => {
                                       var handle = p.Url.GetLastPartUrl();
                                       if (handle.Equals(ProductConstants.TimberPrimerHandle, StringComparison.OrdinalIgnoreCase))
                                           handle = p.Parent?.Parent?.Name == ProductConstants.OutdoorProductType 
                                                        ? ProductConstants.OutdoorTimberPrimerHandle 
                                                        : ProductConstants.IndoorTimberPrimerHandle;
                                       return handle;
                                    },
                                    p => p.Key);
            return dicProduct;
        }
    }
}