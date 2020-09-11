using DuluxGroup.Integrations.HubSpot.Services;
using System;
using System.Threading.Tasks;
using Umbraco.Core.Logging;

namespace HHCoApps.CMSWeb.Services
{
    public class ShopifyOrderService : IShopifyOrderService
    {
        private readonly ILogger _logger;
        private readonly IHubspotService _hubspotService;
        private readonly IHubSpotTokenService _hubSpotTokenService;

        public ShopifyOrderService(ILogger logger, IHubspotService hubspotService, IHubSpotTokenService hubSpotTokenService)
        {
            _logger = logger;
            _hubspotService = hubspotService;
            _hubSpotTokenService = hubSpotTokenService;
        }

        public async Task<bool> UpdateOrderTicketStage(long? orderId, string orderName, Umbraco.Web.PublishedModels.Integration integration)
        {
            try
            {                
                var connectionInformation = _hubSpotTokenService.GetHubSpotConnectionInformation();
                var refreshToken = integration.RefreshToken ?? string.Empty;

                var tokenInfoResponse = await _hubSpotTokenService.CheckMemoryCacheWithTokenInfo(connectionInformation, refreshToken.ToString());
                string orderIdName = $"Order {orderName} ({orderId})";
                
                var result = await _hubspotService.SearchTicket(orderIdName, connectionInformation, tokenInfoResponse);

                if (result.Count == 0)
                {
                    _logger.Error<ShopifyOrderService>($"Search ticket by {orderIdName} not found");
                    return false;
                }
                if (result.Count > 1)
                {
                    _logger.Warn<ShopifyOrderService>($"There are more than one item when searching ticket by {orderIdName}");
                }
                await _hubspotService.UpdateStageTicket(result[0].Id, integration.TicketStage, connectionInformation, tokenInfoResponse);
                _logger.Info<ShopifyOrderService>($"Update ticket stage for {orderIdName} successed");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error<ShopifyOrderService>(ex, $"Update ticket stage for Order {orderName} ({orderId}) failed");
                return false;
            }
        }        
    }

    public interface IShopifyOrderService
    {
        Task<bool> UpdateOrderTicketStage(long? orderId, string orderName, Umbraco.Web.PublishedModels.Integration integration);
    }
}
