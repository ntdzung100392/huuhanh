namespace HHCoApps.CMSWeb.Helpers.CdnUrlResolvers
{
    public class NullCdnUrlResolver : ICdnUrlResolver
    {
        public string GetCdnUrl(string imageUrl)
        {
            return imageUrl;
        }
    }
}