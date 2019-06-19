using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Helpers;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Models.Enums;
using Umbraco.Web;
using Umbraco.Web.Macros;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class ToolAndTipService : IToolAndTipService
    {
        private readonly IMapper _mapper;
        private readonly UmbracoHelper _umbracoHelper;

        public ToolAndTipService(IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
        }

        public ToolsAndTipsViewModel GetViewModel(PartialViewMacroPage macroPage)
        {
            var viewModel = new ToolsAndTipsViewModel();
            var model = macroPage.Model;

            viewModel.IconLinks = GetIconLinksFromSourceItems(model, macroPage);

            return viewModel;
        }

        private IEnumerable<IconLinkModel> GetIconLinksFromSourceItems(PartialViewMacroModel model, PartialViewMacroPage macroPage)
        {
            var contentSourcePropName = model.MacroParameters.GetValue("contentSource", string.Empty);
            if (string.IsNullOrEmpty(contentSourcePropName))
                return Enumerable.Empty<IconLinkModel>();

            var content = macroPage.UmbracoContext.IsFrontEndUmbracoRequest ? _umbracoHelper.Content(macroPage.Model.Content.Id) : model.Content;
            var iconLinks = content.GetProperty(contentSourcePropName)?.Value<IEnumerable<IconLink>>() ?? Enumerable.Empty<IconLink>();
            var iconLinkModels = _mapper.Map<IEnumerable<IconLinkModel>>(iconLinks).ToList();
            foreach (var iconLinkModel in iconLinkModels)
            {
                iconLinkModel.IconCssClass = Icon.FromDisplayName(iconLinkModel.Icon)?.Value;
            }

            return iconLinkModels;
        }
    }

    public interface IToolAndTipService
    {
        ToolsAndTipsViewModel GetViewModel(PartialViewMacroPage macroPage);
    }
}