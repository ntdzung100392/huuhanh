using HHCoApps.CMSWeb.Forms.Constants;
using HHCoApps.CMSWeb.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Web.Composing;
using Umbraco.Web;
using HHCoApps.CMSWeb.Caching;

namespace HHCoApps.CMSWeb.Forms.Workflows
{
    public class HubSpotSubmitShopifyOrderFormWorkFlow : WorkflowType
    {
        private const string HubSpot = "HubSpot";        

        private readonly IRecordStorage _recordStorage;
        private readonly ILogger _logger;
        private readonly IShopifyOrderService _shopifyOrderService;

        public HubSpotSubmitShopifyOrderFormWorkFlow(IRecordStorage recordStorage, IShopifyOrderService shopifyOrderService)
        {
            _recordStorage = recordStorage;
            _logger = Current.Logger;
            _shopifyOrderService = shopifyOrderService;

            Id = Guid.Parse("3A017ADF-85F0-4974-A451-FC35E0FD3569");
            Name = "HubSpot - Submit shopify order";
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs recordEventArgs)
        {
            try
            {
                var orderName = record.GetValue<string>(ShopifyOrderFormConstants.Order);
                var orderId = record.GetValue<long?>(ShopifyOrderFormConstants.Id);                
                if (orderId > 0 && !string.IsNullOrEmpty(orderName))
                {
                    var umbracoHelper = Current.UmbracoHelper;
                    var integration = (Umbraco.Web.PublishedModels.Integration)umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);

                    //Fix bug cannot get ticket stage: System.InvalidOperationException: Could not parse internal links, there is no current UmbracoContext
                    _logger.Info<HubSpotSubmitShopifyOrderFormWorkFlow>($"Ticket stage: {integration.TicketStage}");
                    
                    var isSuccess = Task.Run(async() => await ApproveOrder(record, integration)).Result;
                    if (isSuccess)
                    {
                        record.GetRecordFieldByAlias(ShopifyOrderFormConstants.Status).Values = new List<object>() { ShopifyOrderFormStatus.Fulfillment.ToString() };
                        record.State = FormState.Approved;
                        InsertOrUpdateRecord(record, recordEventArgs);
                        _logger.Info<HubSpotSubmitShopifyOrderFormWorkFlow>($"Approve order {orderId} - {orderName} successed!");
                        return WorkflowExecutionStatus.Completed;
                    }                    
                }
                MarkAsFailed(record, recordEventArgs);                
                return WorkflowExecutionStatus.Failed;
            }
            catch (Exception exception)
            {
                MarkAsFailed(record, recordEventArgs);
                _logger.Error<HubSpotSubmitShopifyOrderFormWorkFlow>(exception, "Cannot submit shopify order form to {Integration}", HubSpot);
                return WorkflowExecutionStatus.Failed;
            }
        }        

        private async Task<bool> ApproveOrder(Record record, Umbraco.Web.PublishedModels.Integration integration)
        {            
            return await _shopifyOrderService.UpdateOrderTicketStage(record.GetValue<long?>(ShopifyOrderFormConstants.Id), record.GetValue<string>(ShopifyOrderFormConstants.Order), integration);            
        }

        private void InsertOrUpdateRecord(Record record, RecordEventArgs e)
        {
            record.IP ??= string.Empty;
            record.RecordData ??= string.Empty;

            if (record.Created == DateTime.MinValue)
            {
                record.Created = DateTime.Now;
                _recordStorage.InsertRecord(record, e.Form);
            }
            else
            {
                record.Updated = DateTime.Now;
                _recordStorage.UpdateRecord(record, e.Form);
            }
        }
        private void MarkAsFailed(Record record, RecordEventArgs recordEventArgs)
        {
            record.State = FormState.Submitted;
            InsertOrUpdateRecord(record, recordEventArgs);
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }
    }
}