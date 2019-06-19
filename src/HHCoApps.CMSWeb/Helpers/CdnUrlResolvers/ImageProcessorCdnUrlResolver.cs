using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Cache;

namespace HHCoApps.CMSWeb.Helpers.CdnUrlResolvers
{
    public class ImageProcessorCdnUrlResolver : ICdnUrlResolver
    {
        private readonly IAppPolicyCache _runtimeCache;

        public static HttpClient HttpClient { get; }

        static ImageProcessorCdnUrlResolver()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            HttpClient = new HttpClient(handler);
        }

        public ImageProcessorCdnUrlResolver(AppCaches appCaches)
        {
            _runtimeCache = appCaches.RuntimeCache;
        }

        public string GetCdnUrl(string imageUrl)
        {
            var cachedCdnUrl = _runtimeCache.GetCacheItem<string>(imageUrl);
            if (!string.IsNullOrEmpty(cachedCdnUrl))
                return cachedCdnUrl;

            var requestUri = HttpContext.Current.Request.Url;
            //Work around for confusing request localhost vs blob in umbraco
            if (requestUri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase) && requestUri.Port == 80)
                return imageUrl;

            Task.Factory.StartNew(() => RetrieveAndCacheCdnUrl(imageUrl, requestUri));
            return imageUrl;
        }

        private async void RetrieveAndCacheCdnUrl(string imageUrl, Uri requestUri)
        {
            try
            {
                var absoluteImageUrl = imageUrl.ToAbsoluteUrl(requestUri);
                var redirectUrl = await GetRedirectedUrl(absoluteImageUrl);
                _runtimeCache.Insert(imageUrl, () => redirectUrl);
            }
            catch (Exception ex)
            {
                throw new Exception(imageUrl + " with host " + requestUri.Host, ex);
            }
        }

        private static async Task<string> GetRedirectedUrl(string url)
        {
            var redirectedUrl = string.Empty;
            using var response = await HttpClient.GetAsync(url);
            using var content = response.Content;

            if (response.StatusCode != System.Net.HttpStatusCode.Found)
                return redirectedUrl;

            var headers = response.Headers;
            if (headers != null && headers.Location != null)
            {
                redirectedUrl = headers.Location.AbsoluteUri;
            }

            return redirectedUrl;
        }
    }
}