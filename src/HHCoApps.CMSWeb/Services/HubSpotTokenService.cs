using DuluxGroup.Integrations.HubSpot.Apis.Models;
using DuluxGroup.Integrations.HubSpot.Services;
using System;
using System.Configuration;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace HHCoApps.CMSWeb.Services
{
    public class HubSpotTokenService : IHubSpotTokenService
    {
        private readonly IAuthenticationService _authenticationService;
        public HubSpotTokenService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<TokenInformation> CheckMemoryCacheWithTokenInfo(ConnectionInformation connectionInformation, string refreshToken)
        {
            var cacheToken = MemoryCache.Default;
            if (!cacheToken.Contains("accessToken"))
            {
                var tokenInfoResponse = await GetTokenInfo(connectionInformation, refreshToken);
                var expiresInMinutes = (tokenInfoResponse.ExpiresIn / 60) - 5;
                cacheToken.Add("accessToken", tokenInfoResponse.AccessToken, DateTimeOffset.Now.AddMinutes(expiresInMinutes));
                cacheToken.Add("expiresIn", tokenInfoResponse.ExpiresIn, DateTimeOffset.Now.AddMinutes(expiresInMinutes));

                return tokenInfoResponse;
            }

            return new TokenInformation
            {
                AccessToken = cacheToken.Get("accessToken").ToString(),
                ExpiresIn = int.Parse(cacheToken.Get("expiresIn").ToString())
            };
        }

        private Task<TokenInformation> GetTokenInfo(ConnectionInformation connectionInformation, string refreshToken)
        {
            _authenticationService.AuthenticationApi.SetSeverConnectionInformation(connectionInformation);
            return _authenticationService.GetTokenInfomationAsync(refreshToken);
        }

        public ConnectionInformation GetHubSpotConnectionInformation()
        {
            return new ConnectionInformation
            {
                BaseUrl = ConfigurationManager.AppSettings["BASE_URL"],
                ClientId = ConfigurationManager.AppSettings["HubSpotClientId"],
                ClientSecret = ConfigurationManager.AppSettings["CLIENT_SECRET"],
                RedirectUri = ConfigurationManager.AppSettings["REDIRECT_URI"]
            };
        }
    }
    public interface IHubSpotTokenService
    {
        Task<TokenInformation> CheckMemoryCacheWithTokenInfo(ConnectionInformation connectionInformation, string refreshToken);
        ConnectionInformation GetHubSpotConnectionInformation();
    }
}