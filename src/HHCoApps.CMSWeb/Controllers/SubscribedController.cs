using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using DuluxGroup.Integration.Mailchimp;
using DuluxGroup.Integration.Mailchimp.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HHCoApps.CMSWeb.Caching;
using Umbraco.Web;
using Umbraco.Web.Composing;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public partial class SubscribedController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMailchimpService _mailchimpService;
        private readonly IMailchimpClient _mailchimpClient;

        public SubscribedController(UmbracoHelper umbracoHelper, IMailchimpService mailchimpService, IMailchimpClient mailchimpClient)
        {
            _umbracoHelper = umbracoHelper;
            _mailchimpClient = mailchimpClient;
            _mailchimpService = mailchimpService;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = CacheTimes.FiveMinutesCacheSeconds, ServerTimeSpan = CacheTimes.FiveMinutesCacheSeconds)]
        public async Task<bool> IsEmailSubscribed([FromUri] ContactInfo contactInfo)
        {
            var integration = (Umbraco.Web.PublishedModels.Integration)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            if (integration == null)
                return false;

            if (contactInfo == null || !contactInfo.Email.IsValidEmailAddress())
                return false;

            var connectionInformation = GetConnectionInformation(integration);
            _mailchimpService.Client.ConnectionInformation = connectionInformation;

            return await _mailchimpService.IsEmailAddressSubscribed(contactInfo.Email, integration.MailchimpListId);
        }

        private ConnectionInformation GetConnectionInformation(Umbraco.Web.PublishedModels.Integration integration)
        {
            return new ConnectionInformation
            {
                BaseAddressUrl = string.Format(ConfigurationManager.AppSettings["MAILCHIMP_API_BASEURL"], integration.MailchimpServer),
                BasicAuth = new BasicAuth("anystring", integration.MailchimpApiKey)
            };
        }
    }
}