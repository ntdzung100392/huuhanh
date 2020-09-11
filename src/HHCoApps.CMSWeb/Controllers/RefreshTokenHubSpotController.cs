using HHCoApps.CMSWeb.Models;
using DuluxGroup.Integrations.HubSpot.Apis.Models;
using DuluxGroup.Integrations.HubSpot.Apis.v1;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    [Authorize]
    public class RefreshTokenHubSpotController : UmbracoAuthorizedApiController
    {
        private readonly UmbracoHelper umbracoHelper;

        public RefreshTokenHubSpotController(UmbracoHelper _umbracoHelper)
        {
            umbracoHelper = _umbracoHelper;
        }

        private ConnectionInformation MapConnectInformation()
        {
            var appSettings = ConfigurationManager.AppSettings;
            return new ConnectionInformation
            {
                ClientId = appSettings.Get("HubSpotClientId"),
                ClientSecret = appSettings.Get("CLIENT_SECRET"),
                BaseUrl = appSettings.Get("BASE_URL"),
                RedirectUri = appSettings.Get("REDIRECT_URI")
            };
        }

        [HttpGet]
        public HubSpotSettings GetHubSpotCredentials()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var hubSpotSettings = new HubSpotSettings
            {
                ClientId = appSettings.Get("HubSpotClientId"),
                Scopes = new[] { appSettings.Get("SCOPE_CONTACT"), appSettings.Get("SCOPE_TICKETS") },
                RedirectUri = appSettings.Get("REDIRECT_URI")
            };

            return hubSpotSettings;
        }

        [HttpPost]
        public async Task<IHttpActionResult> SetRefreshTokenByCode([FromUri] string code)
        {
            var authenticationApi = new AuthenticationApi();
            authenticationApi.SetSeverConnectionInformation(MapConnectInformation());

            var result = await authenticationApi.GetTokenByCode(code);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SaveRefreshTokenToConfiguration(result.RefreshToken);
                return Ok();
            }
            return NotFound();
        }

        private void SaveRefreshTokenToConfiguration(string token)
        {
            var contentService = Services.ContentService;
            var integration = umbracoHelper.ContentQuery.ContentSingleAtXPath("//integration");
            if (integration != null)
            {
                var integrationContent = contentService.GetById(integration.Id);
                integrationContent.SetValue("refreshToken", token);
                contentService.SaveAndPublish(integrationContent);
            }
        }
    }
}