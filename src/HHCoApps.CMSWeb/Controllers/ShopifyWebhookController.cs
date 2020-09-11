using HHCoApps.CMSWeb.Attributes;
using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Forms.Constants;
using HHCoApps.CMSWeb.Services;
using DuluxGroup.Integrations.Shoptify.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [ShopifyAuthorize]
    [JsonCamelCaseFormatter]
    public class ShopifyWebhookController : UmbracoApiController
    {
        private readonly ILogger _logger;               

        private readonly IFormStorage _formStorage;
        private readonly IRecordStorage _recordStorage;
        private readonly IRecordFieldStorage _recordFieldStorage;
        private readonly IShopifyOrderService _shopifyOrderService;
        private readonly UmbracoHelper _umbracoHelper;

        public ShopifyWebhookController(ILogger logger, 
                IContentIndexQueryService queryService, 
                IFormStorage formStorage, 
                IRecordStorage recordStorage,
                IRecordFieldStorage recordFieldStorage,
                IShopifyOrderService shopifyOrderService,
                UmbracoHelper umbracoHelper)
        {
            _logger = logger;             
            _formStorage = formStorage;
            _recordStorage = recordStorage;
            _recordFieldStorage = recordFieldStorage;
            _shopifyOrderService = shopifyOrderService;
            _umbracoHelper = umbracoHelper;
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public async Task<string> OrdersCreate([FromBody] Order obj)
        {
            _logger.Info<ShopifyWebhookController>(obj.OrderStatusUrl);
            var shopifyFormId = ShopifyOrderFormConstants.ShopifyOrderFormId;
            var form = _formStorage.GetForm(shopifyFormId);
            var record = await InsertForm(obj, form);
            var integration = (Umbraco.Web.PublishedModels.Integration)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            bool isUpdated = await _shopifyOrderService.UpdateOrderTicketStage(obj.Id, obj.Name, integration);
            if (isUpdated)
            {
                UpdateForm(record, form);
            }
            return obj.OrderStatusUrl;
        }
        
        private async Task<Record> InsertForm(Order obj, Form form)
        {           
            var record = new Record();                    
            foreach (var field in form.AllFields)
            {
                switch (field.Alias)
                {
                    case ShopifyOrderFormConstants.Order:
                        AddFieldToRecord(field, record, obj.Name);
                        break;
                    case ShopifyOrderFormConstants.Date:
                        AddFieldToRecord(field, record, obj.CreatedAt?.ToString("MM/dd/yyyy HH:mm"));
                        break;
                    case ShopifyOrderFormConstants.CustomerName:
                        AddFieldToRecord(field, record, $"{obj.Customer?.FirstName} {obj.Customer?.LastName}");
                        break;
                    case ShopifyOrderFormConstants.CustomerEmail:
                        AddFieldToRecord(field, record, obj.Customer?.Email);
                        break;
                    case ShopifyOrderFormConstants.Total:
                        AddFieldToRecord(field, record, obj.TotalPrice);
                        break;
                    case ShopifyOrderFormConstants.Fulfillment:
                        AddFieldToRecord(field, record, obj.FulfillmentStatus);
                        break;
                    case ShopifyOrderFormConstants.Items:
                        AddFieldToRecord(field, record, obj.LineItems?.Sum(x => x.Quantity));
                        break;
                    case ShopifyOrderFormConstants.Id:
                        AddFieldToRecord(field, record, obj.Id);
                        break;
                    case ShopifyOrderFormConstants.Status:
                        AddFieldToRecord(field, record, ShopifyOrderFormStatus.Create.ToString());
                        break;
                    case ShopifyOrderFormConstants.Detail:
                        var detail = await Request.Content.ReadAsStringAsync();
                        AddFieldToRecord(field, record, detail);
                        break;
                    default:
                        break;
                }
            }
                    
            record.RecordData = record.GenerateRecordDataAsJson();
            record.Form = form.Id; ;
            record.UmbracoPageId = 1285; //ID home page
            record.State = FormState.Submitted;
            return _recordStorage.InsertRecord(record, form);
        }

        private void UpdateForm(Record record, Form form)
        {
            record.GetRecordFieldByAlias(ShopifyOrderFormConstants.Status).Values = new List<object>() { ShopifyOrderFormStatus.Fulfillment.ToString() };
            record.State = FormState.Approved;
            _recordStorage.UpdateRecord(record, form);
        }

        public void AddFieldToRecord(Field field, Record record, object value)
        {
            var key = Guid.NewGuid();
            var recordField = new RecordField()
            {
                Key = key,
                FieldId = field.Id,
                Record = record.Id,
                Alias = field.Alias,
                Field = field,
                Values = new List<object>() { value }
            };
            _recordFieldStorage.InsertRecordField(recordField);
            record.RecordFields.Add(key, recordField);
        }       
    }
}