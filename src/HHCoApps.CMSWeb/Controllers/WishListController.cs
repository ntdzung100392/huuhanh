using HHCoApps.CMSWeb.Caching;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.RequestModels;
using HHCoApps.CMSWeb.Services;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using WebApi.OutputCache.V2;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class WishListController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IEmailTemplateService _emailTemplateService;

        public WishListController(UmbracoHelper umbracoHelper, IEmailTemplateService emailTemplateService)
        {
            _umbracoHelper = umbracoHelper;
            _emailTemplateService = emailTemplateService;
        }

        [HttpPost]
        [CacheOutput(ClientTimeSpan = CacheTimes.OneHourCacheSeconds, ServerTimeSpan = CacheTimes.OneHourCacheSeconds)]
        public async Task<WishListEmailResponse> SendWishListEmail([FromBody] WishListEmailRequest wishList)
        {
            var rootUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

            if (string.IsNullOrWhiteSpace(wishList.FirstName))
                return new WishListEmailResponse { IsSuccess = false, Message = "Please enter first name" };

            if (string.IsNullOrWhiteSpace(wishList.LastName))
                return new WishListEmailResponse { IsSuccess = false, Message = "Please enter last name" };

            if (string.IsNullOrWhiteSpace(wishList.Email) || !wishList.Email.IsValidEmailAddress())
                return new WishListEmailResponse { IsSuccess = false, Message = "Please enter a valid email address" };

            var integration = (Umbraco.Web.PublishedModels.Integration)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            if (integration == null)
                throw new ArgumentNullException(nameof(integration));

            var sendGridApi = integration.SendGridApiKey ?? string.Empty;
            if (string.IsNullOrWhiteSpace(sendGridApi))
                throw new ArgumentNullException(nameof(sendGridApi));

            if (string.IsNullOrWhiteSpace(integration.FromEmail) || string.IsNullOrWhiteSpace(integration.EmailSubject))
                throw new ArgumentNullException(nameof(integration.FromEmail));

            var navigationManagement = (NavigationManagement)_umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.NavigationManagementContent);
            if (navigationManagement == null)
                throw new ArgumentNullException(nameof(navigationManagement));

            var model = _emailTemplateService.GetWithListViewModel(integration, navigationManagement, wishList, rootUrl);
            var emailBody = _emailTemplateService.GenerateHtml(model);

            var sendGridClient = new SendGridClient(integration.SendGridApiKey);
            var senderInfo = new EmailAddress(model.FromEmail, model.FromContactName);
            var receiverInfo = new EmailAddress(model.ToEmail, model.ToName);

            var sendGridMessage = MailHelper.CreateSingleEmail(senderInfo, receiverInfo, model.EmailSubject, string.Empty, emailBody);
            var response = await sendGridClient.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                return new WishListEmailResponse { IsSuccess = true, Message = string.Empty };

            var errorData = JsonConvert.DeserializeObject<SendGridEmailError>(response.Body.ReadAsStringAsync().Result);
            var errorMessage = errorData.Errors != null && errorData.Errors.Length > 0 ?
                                errorData.Errors[0].Message : "Sorry, an error has occurred. Please try again";

            return new WishListEmailResponse
            {
                IsSuccess = false,
                Message = errorMessage
            };
        }
    }
}