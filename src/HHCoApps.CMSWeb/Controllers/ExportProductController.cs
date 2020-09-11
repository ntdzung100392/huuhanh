using CsvHelper;
using HHCoApps.CMSWeb.Helpers;
using DuluxGroup.Integrations.Shoptify.Models.Imports;
using DuluxGroup.Plugins.Tools.Helpers;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using HHCoApps.CMSWeb.Models.Constants;
namespace HHCoApps.CMSWeb.Controllers
{
    public class ExportProductController : UmbracoApiController
    {
        private readonly IPublishedContentQuery _publishedContentQuery;        

        public ExportProductController(IPublishedContentQuery publishedContentQuery)
        {
            _publishedContentQuery = publishedContentQuery;
        }

        [HttpGet]
        public Task<IHttpActionResult> Export([FromUri] string uid)
        {
            var product = _publishedContentQuery.Content(Guid.Parse(uid)) as Product;
            var records = Create(product);
            var data = WriteCsvToMemory(records);
            var result = new DownloadResult(data, Request, product.Url.GetLastPartUrl() + ".csv");
            return Task.FromResult<IHttpActionResult>(result);
        }

        [HttpGet]
        public Task<IHttpActionResult> ExportByCategoryId([FromUri] string uid)
        {
            var productCategory = _publishedContentQuery.Content(Guid.Parse(uid)) as Products;
            
            var lstProduct = productCategory.Descendants<Product>()
                                            .Where(x => !x.HideInSideNavigation && x.Id != ProductConstants.OutdoorTimberPrimerId) //Ignore duplicate Timber Primer
                                            .OrderBy(x=>x.Name).ToList();
            var lstShopifyVariants = new List<ShopifyVariant>();
            foreach(Product product in lstProduct)
            {
                lstShopifyVariants.AddRange(Create(product));
            }
            AddOutdoorTimberPrimerVariants(lstShopifyVariants);      
            var data = WriteCsvToMemory(lstShopifyVariants.OrderBy(x =>x.Handle).ToList());
            var result = new DownloadResult(data, Request, "category-" + productCategory.Url.GetLastPartUrl() + ".csv");
            return Task.FromResult<IHttpActionResult>(result);
        }

        private List<ShopifyVariant> Create(Product product)
        {         
            var lstProductImport = new List<ShopifyVariant>();
            string handle = product.Url.GetLastPartUrl();
            ShopifyVariant firstVariant = new ShopifyVariant(handle);
            firstVariant.Title = product.PageTitle;            
            var summaryInHtml = product.Summary.SelectToken("sections[0].rows[0].areas[0].controls[0].value")?.ToString();
            if (!string.IsNullOrWhiteSpace(summaryInHtml))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(summaryInHtml);
                firstVariant.BodyHTML = htmlDoc.DocumentNode.SelectSingleNode("//body")?.InnerHtml;
            }            
            firstVariant.Published = "true";
            firstVariant.Option1Name = "Size";            
            firstVariant.ImageSrc = "https://www.feastwatson.com.au" + product.Images.FirstOrDefault().Url;
            firstVariant.ImagePosition = 1;
            firstVariant.GiftCard = "false";
            firstVariant.Type = product.Parent?.Parent?.Name;
            bool isFirst = true;

            var products = GetProductFromClient();
            products.ForEach(x => x.Name = x.Name.ToLower().Replace("&", "").Replace(" ", "-").Replace("--", "-"));
            var productFromClient = products.FirstOrDefault(x => x.Name.Equals(handle, StringComparison.OrdinalIgnoreCase));

            if (product.Colors.Count() == 0)
            {
                lstProductImport.Add(CreateVariants(firstVariant, product.AvailableSizes.FirstOrDefault(), string.Empty, productFromClient));
                foreach (var size in product.AvailableSizes.Skip(1))
                {
                    lstProductImport.Add(CreateVariants(new ShopifyVariant(handle), size, string.Empty, productFromClient));
                }                
            }
            else
            {               
                foreach (var size in product.AvailableSizes)
                {
                    foreach (var color in product.Colors)
                    {
                        var colorName = color.AvailableColor.Name;                        
                        if (isFirst)
                        {
                            lstProductImport.Add(CreateVariants(firstVariant, size, colorName, productFromClient));
                            isFirst = false;
                        }
                        else
                        {
                            lstProductImport.Add(CreateVariants(new ShopifyVariant(handle), size, colorName, productFromClient));
                        }
                    }
                }
            }            
            return lstProductImport;
        }

        private void AddOutdoorTimberPrimerVariants(List<ShopifyVariant> variants)
        {            
            var outdoorTimberPrimerVariants = variants.Where(v => v.Handle == ProductConstants.TimberPrimerHandle)
                            .Select(v =>
                            {
                                var variant = new ShopifyVariant(ProductConstants.OutdoorTimberPrimerHandle);
                                var props = v.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                               .Where(prop => prop.Name != nameof(ShopifyVariant.Handle) && prop.Name != nameof(ShopifyVariant.VariantSKU));                                               
                                foreach (var prop in props)
                                {
                                    variant.GetType().GetProperty(prop.Name).SetValue(variant, prop.GetValue(v,null));
                                }
                                variant.VariantSKU = v.VariantSKU.Replace(ProductConstants.TimberPrimerHandle, ProductConstants.OutdoorTimberPrimerHandle);
                                variant.Type = ProductConstants.OutdoorProductType;
                                return variant;
                            }                            
                            ).ToList();
            variants.AddRange(outdoorTimberPrimerVariants);            
        }

        private ShopifyVariant CreateVariants(ShopifyVariant shopifyVariant, string size, string colour, ProductFromClient product)
        {            
            shopifyVariant.Option1Value = size;
            if (!string.IsNullOrEmpty(colour))
            {
                shopifyVariant.Option2Name = "Color";
                shopifyVariant.Option2Value = colour;
            }
            shopifyVariant.VariantGrams = 0;
            shopifyVariant.VariantInventoryTracker = "shopify";
            shopifyVariant.VariantInventoryQty = 0;
            shopifyVariant.VariantInventoryPolicy = "continue";
            shopifyVariant.VariantFulfillmentService = "manual";
            shopifyVariant.VariantRequiresShipping = true;

            if (product != null)
            {
                var sizeColorKey = (size + (string.IsNullOrEmpty(colour) ? string.Empty : "-" + colour)).ToLower();
                shopifyVariant.VariantPrice = product.Prices.ContainsKey(sizeColorKey) ? product.Prices[sizeColorKey] : 0;
                shopifyVariant.VariantSKU = product.Skus.ContainsKey(sizeColorKey) ? product.Skus[sizeColorKey]
                                                                                   : shopifyVariant.Handle + "_" + size + (string.IsNullOrEmpty(colour) ? string.Empty : "_" + colour.Replace(' ', '-'));
            }
            else
            {
                shopifyVariant.VariantPrice = 0;
                shopifyVariant.VariantSKU = shopifyVariant.Handle + "_" + size + (string.IsNullOrEmpty(colour) ? string.Empty : "_" + colour.Replace(' ', '-'));
            }

            return shopifyVariant;
        }
        private List<ProductFromClient> GetProductFromClient()
        {
            var products = new List<ProductFromClient>();
            var files = HttpContext.Current.Request.Files;
            if (files.Count > 0 && files[0] != null && files[0].ContentLength > 0)
            {
                var importFile = files[0];
                string directory = HostingEnvironment.MapPath("~/App_Data/TEMP/FileUploads/");
                string path = Path.Combine(directory, Guid.NewGuid().ToString() + "WebsiteContent.json");
                importFile.SaveAs(path);
                products = JsonConvert.DeserializeObject<List<ProductFromClient>>(System.IO.File.ReadAllText(path));                
            }
            return products;
        }
        private byte[] WriteCsvToMemory(List<ShopifyVariant> records)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.RegisterClassMap<ShopifyVariantClassMap>();
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

    }

}