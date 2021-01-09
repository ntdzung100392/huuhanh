using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Web.Composing;
using Umbraco.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Umbraco.Core.Logging;
using System.Web.Http.Controllers;
using HHCoApps.CMSWeb.Caching;

namespace HHCoApps.CMSWeb.Attributes
{
    public class ShopifyAuthorizeAttribute : AuthorizeAttribute
    {

        private IPublishedContentQuery PublishedContentQuery => Umbraco.Core.Composing.Current.Factory.GetInstance<IPublishedContentQuery>();
        private ILogger Logger => Current.Logger;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var integration = (Umbraco.Web.PublishedModels.Integration)PublishedContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
                var requestHeaders = actionContext.Request.Headers;
                var hmacHeaderValue = requestHeaders.FirstOrDefault(kvp => kvp.Key.Equals("X-Shopify-Hmac-SHA256", StringComparison.OrdinalIgnoreCase)).Value.FirstOrDefault();

                if (string.IsNullOrEmpty(hmacHeaderValue))
                {
                    return false;
                }
                string hmacHeader = hmacHeaderValue;
                HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(integration.WebhookSign));
                string hash = string.Empty;
                using (var stream = new MemoryStream())
                {
                    var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
                    context.Request.InputStream.Seek(0, SeekOrigin.Begin);
                    context.Request.InputStream.CopyTo(stream);
                    var requestBody = stream.ToArray();
                    hash = Convert.ToBase64String(hmac.ComputeHash(requestBody));
                }
                bool re = hash == hmacHeader;
                if (!re)
                {
                    Logger.Error<ShopifyAuthorizeAttribute>("Hmac is not match");
                }
                return re;
            }
            catch (Exception ex)
            {
                Logger.Error<ShopifyAuthorizeAttribute>(ex);
                return false;
            }
        }

    }
}