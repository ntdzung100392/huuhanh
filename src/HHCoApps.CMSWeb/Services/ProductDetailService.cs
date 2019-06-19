using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Macros;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMapper _mapper;

        public ProductDetailService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductSummaryViewModel GetSummaryViewModelFromComponent(ProductSummary model, IPublishedContent assignedContentItem)
        {
            var viewModel = new ProductSummaryViewModel
            {
                DownloadLinks = model.DownloadLinks.Any() ? model.DownloadLinks : Enumerable.Empty<Link>()
            };

            if (assignedContentItem is Product product)
            {
                var images = product.Images;
                viewModel.Images = _mapper.Map<IEnumerable<ImageModel>>(images);
                viewModel.Title = string.IsNullOrEmpty(model.Title) ? product.PageTitle : model.Title;
                viewModel.Summary = product.Summary;
            }

            viewModel.AddToWishListEnabled = model.AddToWishListEnabled;
            viewModel.FindAStockistEnabled = model.FindAstockist;

            return viewModel;
        }

        public ProductDetailViewModel GetProductDetailFromMacro(PartialViewMacroPage macroPage)
        {
            var viewModel = new ProductDetailViewModel();
            if (macroPage.Model.Content is Product product)
            {
                viewModel.Tabs = product.Tabs.Any() ? product.Tabs.ToArray() : new TabItem[0];
            }
            return viewModel;
        }
    }

    public interface IProductDetailService
    {
        ProductSummaryViewModel GetSummaryViewModelFromComponent(ProductSummary model, IPublishedContent umbracoAssignedContentItem);
        ProductDetailViewModel GetProductDetailFromMacro(PartialViewMacroPage macroPage);
    }
}