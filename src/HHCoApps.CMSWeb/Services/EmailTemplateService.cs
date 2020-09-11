using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Helpers.Enum;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using HHCoApps.CMSWeb.Models.RequestModels;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Linq;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private static IRazorEngineService _razorEngine;
        private const string templateKey = "EmailTemplateKey";

        static EmailTemplateService()
        {
            var emailTemplatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Views/Partials/WishList/EmailTemplate.cshtml");
            var templateAsString = System.IO.File.ReadAllText(emailTemplatePath);
            _razorEngine = RazorEngineService.Create();
            _razorEngine.AddTemplate(templateKey, new LoadedTemplateSource(templateAsString));
            _razorEngine.Compile(templateKey, typeof(EmailTemplateViewModel));
        }

        public string GenerateHtml(EmailTemplateViewModel model)
        {
            return _razorEngine.Run(templateKey, typeof(EmailTemplateViewModel), model);
        }

        public EmailTemplateViewModel GetWithListViewModel(Umbraco.Web.PublishedModels.Integration integration, NavigationManagement navigationManagement, WishListEmailRequest wishListEmailRequest, string rootUrl)
        {
            var emailSubject = integration.EmailSubject ?? string.Empty;
            var senderEmail = integration.FromEmail ?? string.Empty;
            var senderName = integration.FromContactName ?? string.Empty;
            var toName = $"{wishListEmailRequest.FirstName} {wishListEmailRequest.LastName}";
            var headerBannerLogo = integration.HeaderLogoImage.GetCropImageUrl(ImageCropProfile.EmailHeaderBannerLogo).ToAbsoluteUrl();
            var headerBannerImage = integration.HeaderBannerImage.GetCropImageUrl(ImageCropProfile.EmailHeaderBannerImage).ToAbsoluteUrl();
            var headerDescription = integration.EmailHeaderDescription ?? string.Empty;
            var footerBannerImage = integration.FooterBannerImage.GetCropImageUrl(ImageCropProfile.EmailFooterBannerImage).ToAbsoluteUrl();
            var findYourStockistDescription = integration.FindYourStockistDescription ?? string.Empty;
            var findYourStockistLink = integration.FindYourStockistLink?.Url.ToAbsoluteUrl() ?? string.Empty;

            var socialNetworkContentInfos = navigationManagement?.SocialNetworks ?? Enumerable.Empty<IconLink>();
            var socialNetworkIcons = socialNetworkContentInfos.Select(socialNetwork => new SocialLink
            {
                ImgUrl = !string.IsNullOrEmpty(socialNetwork?.Icon) ? $"{rootUrl}/assets/images/icons/{ GetSocialIconName(socialNetwork)}.png".GetCropImageUrl(29, 28, ImageCropProfile.IconLink) : string.Empty,
                LinkUrl = socialNetwork?.Link?.Url ?? string.Empty
            });

            return new EmailTemplateViewModel
            {
                EmailSubject = emailSubject,
                FromEmail = senderEmail,
                FromContactName = senderName,
                ToEmail = wishListEmailRequest.Email,
                ToName = toName,
                HeaderBannerImage = headerBannerImage,
                HeaderBannerLogo = headerBannerLogo,
                FooterBannerImage = footerBannerImage,
                HeaderDescription = headerDescription,
                FindYourStockistDescription = findYourStockistDescription,
                FindYourStockistLink = findYourStockistLink,
                WishListItems = wishListEmailRequest.wishListItems,
                SocialNetworks = socialNetworkIcons
            };
        }

        private string GetSocialIconName(IconLink link)
        {
            var iconLinkText = link.Icon ?? string.Empty;
            var returnValue = string.Empty;

            switch (iconLinkText)
            {
                case "Social Facebook":
                    returnValue = "icon-fb";
                    break;
                case "Social Instagram":
                    returnValue = "icon-insta";
                    break;
                case "Social Pinterest":
                    returnValue = "icon-pinterest-email";
                    break;
                case "Social Youtube":
                    returnValue = "icon-youtube";
                    break;
            }

            return returnValue;
        }
    }

    public interface IEmailTemplateService
    {
        EmailTemplateViewModel GetWithListViewModel(Umbraco.Web.PublishedModels.Integration integration, NavigationManagement navigationManagement, WishListEmailRequest wishListEmailRequest, string rootUrl);
        string GenerateHtml(EmailTemplateViewModel model);
    }
}