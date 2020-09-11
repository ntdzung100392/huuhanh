using CsvHelper;
using DuluxGroup.Integrations.Shoptify.Models.Imports;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using Umbraco.Web;
using System;
using Umbraco.Web.PublishedModels;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Hosting;
using DuluxGroup.Plugins.Tools.Helpers;
using HHCoApps.CMSWeb.Models.Constants;
namespace HHCoApps.CMSWeb.Controllers
{
    public class ImportProductShopifyMappingController : UmbracoApiController
    {        
        private readonly IContentService _contentService;
        private readonly IPublishedContentQuery _publishedContentQuery;
        
        public ImportProductShopifyMappingController(IPublishedContentQuery publishedContentQuery, IContentService contentService)
        {
            _publishedContentQuery = publishedContentQuery;
            _contentService = contentService;
        }
        [HttpPost]
        public Task<IHttpActionResult> Import()
        {
            try
            {
                var files = HttpContext.Current.Request.Files;
                if (files.Count > 0 && files[0] != null && files[0].ContentLength > 0)
                {
                    var importFile = files[0];
                    StringBuilder message = new StringBuilder();
                    string directory = HostingEnvironment.MapPath("~/App_Data/TEMP/FileUploads/");
                    string path = Path.Combine(directory, Path.GetFileName(Guid.NewGuid().ToString() + importFile.FileName));
                    importFile.SaveAs(path);
                    
                    var dicProduct = GetProductDitionary();
                    var dicColour = GetColorDictionary();

                    using (TextReader fileReader = System.IO.File.OpenText(path))
                    using (var csvReader = new CsvReader(fileReader, culture: CultureInfo.InvariantCulture))
                    {
                        csvReader.Configuration.RegisterClassMap<ShopifyVariantClassMap>();
                        var records = csvReader.GetRecords<ShopifyVariant>();                        
                        var productGroup = records.GroupBy(x => x.Handle);
                        foreach (var productVariant in productGroup)
                        {
                            if (dicProduct.ContainsKey(productVariant.Key))
                            {
                                var productContent = _contentService.GetById(dicProduct[productVariant.Key]);
                                productContent.SetValue("shopifyMapping", GetShopifyMappingValue(productVariant, dicColour));
                                _contentService.SaveAndPublish(productContent);
                                message.Append($"Add mapping for {productVariant.Key} successed. ").Append(Environment.NewLine);
                            }
                            else
                            {
                                message.Append($"Product {productVariant.Key} is not exited").Append(Environment.NewLine);
                            }
                        }
                    }
                    System.IO.File.Delete(path);
                    var (isSuccess, updateInfo) = UpdateOutdoorTimberPrimer();
                    message.Append(updateInfo);
                    return Task.FromResult<IHttpActionResult>(Ok(message.ToString()));
                }
                else
                {
                    return Task.FromResult<IHttpActionResult>(BadRequest("File import is not found or invalid"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult<IHttpActionResult>(BadRequest("Import error: " + ex.Message));
            }                      
        }
        private Dictionary<string, Guid> GetProductDitionary()
        {
            var products = _publishedContentQuery.Content(ProductConstants.ProductsNodeId) as Products;
            var dicProduct = products.Descendants<Product>()
                                   .Where(p => !p.HideInSideNavigation && p.Id != ProductConstants.OutdoorTimberPrimerId)
                                   .OrderBy(p => p.Name)
                                   .ToDictionary(p => p.Url.GetLastPartUrl(), p => p.Key);
            return dicProduct;
        }

        private Dictionary<string, string> GetColorDictionary()
        {                        
            var globalColourList = _publishedContentQuery.Content(ProductConstants.GlobalColourListId) as GlobalColourList;
            var dicColour = globalColourList.Descendants<Colour>()
                                            .ToList()
                                            .ToDictionary(color => color.ColourName.ToLower(), 
                                                          color => $"umb://document/{color.Key.ToString().Replace("-", "").ToLower()}");
            return dicColour;
        }

        private JArray GetShopifyMappingValue(IGrouping<string, ShopifyVariant> shopifyProduct, Dictionary<string, string> dicColour)
        {
            var shopifyMappingValue = new JArray();
            foreach (var variant in shopifyProduct)
            {
                var nestedContentItem = new JObject();
                nestedContentItem.Add("key", Guid.NewGuid());
                nestedContentItem.Add("name", $"[\"{variant.Option1Value}\"] {variant.Option2Value} : {variant.VariantSKU}");
                nestedContentItem.Add("ncContentTypeAlias", "productVariantMapping");
                nestedContentItem.Add(nameof(ProductVariantMapping.Sizes).ToLower(), JArray.Parse($"['{variant.Option1Value}']"));
                nestedContentItem.Add(nameof(ProductVariantMapping.Colors).ToLower(), variant.Option2Value != null && dicColour.ContainsKey(variant.Option2Value.ToLower())
                                                                                        ? dicColour[variant.Option2Value.ToLower()] : null);
                nestedContentItem.Add("shopifyValue", variant.VariantSKU);
                shopifyMappingValue.Add(nestedContentItem);
            }
            return shopifyMappingValue;
        }
        
        [HttpPost]
        public Task<IHttpActionResult> Reset()
        {
            var dicProduct = GetProductDitionary();
            foreach (var item in dicProduct)
            {
                var productContent = _contentService.GetById(item.Value);
                productContent.SetValue("shopifyMapping", null);
                _contentService.SaveAndPublish(productContent);
            }
            return Task.FromResult<IHttpActionResult>(Ok("Success"));
        }
        [HttpPost]
        public Task<IHttpActionResult> SetMappingsForOutDoorTimberPrimer()
        {
            var (isSuccess, message) = UpdateOutdoorTimberPrimer();
            if (isSuccess)
            {
                return Task.FromResult<IHttpActionResult>(Ok(message));
            }
            else
            {
                return Task.FromResult<IHttpActionResult>(BadRequest(message));
            }
            
        }

        private (bool isSuccess, string message) UpdateOutdoorTimberPrimer()
        {
            try
            {
                var products = _publishedContentQuery.Content(ProductConstants.ProductsNodeId) as Products;
                var timberOutdoor = products.Descendants<Product>().SingleOrDefault(p => p.Id == ProductConstants.OutdoorTimberPrimerId);
                var timberIndoor = products.Descendants<Product>().SingleOrDefault(p => p.Id == ProductConstants.IndoorTimberPrimerId);
                if (timberIndoor != null && timberOutdoor != null)
                {
                    var productContent = _contentService.GetById(timberOutdoor.Key);

                    var shopifyMappingValue = new JArray();
                    foreach (var mapping in timberIndoor.ShopifyMapping)
                    {
                        var nestedContentItem = new JObject();
                        nestedContentItem.Add("key", Guid.NewGuid());
                        nestedContentItem.Add("name", $"[\"{mapping.Sizes}\"] {mapping.Colors?.Name} : {mapping.ShopifyValue}");
                        nestedContentItem.Add("ncContentTypeAlias", "productVariantMapping");
                        nestedContentItem.Add(nameof(ProductVariantMapping.Sizes).ToLower(), JArray.Parse($"['{mapping.Sizes}']"));
                        nestedContentItem.Add(nameof(ProductVariantMapping.Colors).ToLower(), mapping.Colors?.Name);
                        nestedContentItem.Add("shopifyValue", mapping.ShopifyValue.Replace(ProductConstants.TimberPrimerHandle, ProductConstants.OutdoorTimberPrimerHandle));
                        shopifyMappingValue.Add(nestedContentItem);
                    }

                    productContent.SetValue("shopifyMapping", shopifyMappingValue);
                    _contentService.SaveAndPublish(productContent);
                    return (true, "Update outdoor timber primer success");
                }
                else
                {
                    return (false, "Update outdoor timber primer failed: Not found both of timber product");
                }
            }
            catch (Exception ex)
            {
                return (false, "Update outdoor timber primer failed, detail: " + ex.Message);
            }
            
        }
    }
}