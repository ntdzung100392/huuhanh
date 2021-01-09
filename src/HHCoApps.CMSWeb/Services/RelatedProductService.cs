using AutoMapper;
using Flurl;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class RelatedProductService : IRelatedProductService
    {
        private readonly IMapper _mapper;
        private readonly UmbracoHelper _umbracoHelper;

        public RelatedProductService(IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
        }

        public RelatedProductViewModel GetViewModel(IPublishedContent model)
        {
            if (model == null)
                return new RelatedProductViewModel();

            var contentModel = (Article)model;
            var contentInfo = new RelatedProductViewModel();
            var relatedProductModels = new List<RelatedProductModel>();

            foreach (var relatedProduct in contentModel.RelatedProducts)
            {
                if (relatedProduct.Link is Product)
                {
                    var productContent = _umbracoHelper.Content(relatedProduct.Link.Id);

                    var relatedProductModel = _mapper.Map<RelatedProductModel>(productContent);
                    relatedProductModel.Title = !string.IsNullOrEmpty(relatedProduct.Label) ? relatedProduct.Label : relatedProductModel.Title;

                    if (!string.IsNullOrEmpty(relatedProductModel.ProductUrl) && relatedProduct.Colour != null)
                    {
                        var colorName = relatedProduct.Colour.GetProperty("colourName")?.Value().ToString() ?? string.Empty;
                        relatedProductModel.ProductUrl = relatedProductModel.ProductUrl.SetQueryParam("selectedColor", colorName.ToLower()).ToString(true);
                    }
                    relatedProductModels.Add(relatedProductModel);
                }
            }

            contentInfo.RelatedProducts = relatedProductModels;

            return contentInfo;
        }
    }

    public interface IRelatedProductService
    {
        RelatedProductViewModel GetViewModel(IPublishedContent componentModel);
    }
}