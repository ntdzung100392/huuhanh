using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class GridLayoutExtensions
    {
        public static MvcHtmlString GetGridHtml(this HtmlHelper html, IPublishedElement publishedElement, string propertyAlias, string gridTemplate)
        {
            if (propertyAlias == null)
            {
                return new MvcHtmlString("");
            }

            var model = publishedElement
                .GetProperty(propertyAlias)
                .GetValue();

            return html.Partial($"~/Views/Partials/Grid/{gridTemplate}.cshtml", model);
        }

        public static MvcHtmlString GetGridHtml(this HtmlHelper html, JToken gridValue, string gridTemplate)
        {
            if (gridValue == null)
            {
                return new MvcHtmlString("");
            }

            return html.Partial($"~/Views/Partials/Grid/{gridTemplate}.cshtml", gridValue);
        }
    }
}