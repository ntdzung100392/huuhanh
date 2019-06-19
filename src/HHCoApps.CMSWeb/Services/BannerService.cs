using System;
using System.Web;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Macros;
using Umbraco.Web.Models;

namespace HHCoApps.CMSWeb.Services
{
    public class BannerService : IBannerService
    {
        private readonly UmbracoHelper _umbracoHelper;

        public BannerService(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public BannerViewModel GetViewModel(PartialViewMacroPage macroPage)
        {
            var viewModel = new BannerViewModel();
            var model = macroPage.Model;

            var content = macroPage.UmbracoContext.IsFrontEndUmbracoRequest ? _umbracoHelper.Content(macroPage.Model.Content.Id) : model.Content;
            var labelSourcePropName = model.MacroParameters.GetValue("labelSource", string.Empty);
            var label = model.MacroParameters.GetValue("label", string.Empty);
            viewModel.Label = !string.IsNullOrEmpty(label) ? label : content.GetProperty(labelSourcePropName)?.Value().ToString() ?? string.Empty;

            var image = GetViewMoreContent(model, content);
            var imageSourcePropName = model.MacroParameters.GetValue("imageSource", string.Empty);
            viewModel.ImageUrl = image?.Url ?? content.GetProperty(imageSourcePropName)?.Value<IPublishedContent>()?.Url ?? string.Empty;

            var descriptionSourcePropName = model.MacroParameters.GetValue("descriptionSource", string.Empty);
            var description = model.MacroParameters.GetValue("description", string.Empty);
            viewModel.DescriptionHtml = !string.IsNullOrEmpty(description) ? description : content.GetProperty(descriptionSourcePropName)?.Value<IHtmlString>()?.ToHtmlString() ?? string.Empty;

            var navigationLinkSourcePropName = model.MacroParameters.GetValue("navigationLinkSource", string.Empty);
            var navigationLink = content.GetProperty(navigationLinkSourcePropName)?.Value<Link>() ?? new Link();

            var goToLabel = model.MacroParameters.GetValue("goToLabel", string.Empty);
            var gotoUrl = model.MacroParameters.GetValue("goToLink", string.Empty);

            viewModel.NavigationTitle = !string.IsNullOrEmpty(goToLabel) ? goToLabel : navigationLink.Name;
            viewModel.NavigationUrl = !string.IsNullOrEmpty(gotoUrl) ? gotoUrl : navigationLink.Url;

            var imageOnTheLeft = model.MacroParameters.GetValue("imageOnTheLeft", string.Empty);
            viewModel.ImagePosition = imageOnTheLeft.Equals("1", StringComparison.OrdinalIgnoreCase) ? Position.Left : Position.Right;

            viewModel.Title = content.GetProperty("pageTitle")?.Value().ToString() ?? string.Empty;
            viewModel.SubTitle = content.GetProperty("brandSubTitle")?.Value().ToString() ?? string.Empty;
            viewModel.Introduction = content.GetProperty("description")?.Value().ToString() ?? string.Empty;
            viewModel.PageTitle = content.GetProperty("pageTitle")?.Value().ToString() ?? string.Empty;
            viewModel.ParentPageTitle = navigationLink?.Name ?? string.Empty;
            var brandImageSourcePropName = model.MacroParameters.GetValue("imageSource", string.Empty);
            viewModel.BrandImageUrl = content.GetProperty(brandImageSourcePropName)?.Value<IPublishedContent>()?.Url ?? string.Empty;

            return viewModel;
        }

        public IPublishedContent GetViewMoreContent(PartialViewMacroModel model, IPublishedContent content)
        {
            var imageUmd = model.MacroParameters.GetValue("image", string.Empty);

            if (string.IsNullOrEmpty(imageUmd))
                return null;

            return _umbracoHelper.Media(imageUmd);
        }
    }

    public interface IBannerService
    {
        BannerViewModel GetViewModel(PartialViewMacroPage macroPage);
    }
}