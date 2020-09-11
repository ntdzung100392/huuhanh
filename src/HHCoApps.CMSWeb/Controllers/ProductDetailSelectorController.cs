using AutoMapper;
using HHCoApps.CMSWeb.Models;
using HHCoApps.CMSWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.WebApi;
using System.Web.Http;
using HHCoApps.CMSWeb.Helpers.Enum;
using HHCoApps.CMSWeb.Models.RequestModels;
using Flurl;
using Umbraco.Core.Models.PublishedContent;

namespace HHCoApps.CMSWeb.Controllers
{
    [JsonCamelCaseFormatter]
    public class ProductDetailSelectorController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMapper _mapper;

        public ProductDetailSelectorController(IMapper mapper, UmbracoHelper umbracoHelper)
        {
            _mapper = mapper;
            _umbracoHelper = umbracoHelper;
        }

        private CoatModel GetRawCoat(Timber timber)
        {
            return new CoatModel { CoatOption = "Raw", CoatImageUrl = timber.GetCropImageUrl(ImageCropProfile.CoatImage) };
        }

        [HttpGet]
        public ProductDetailSelectorViewModel GetProductDetailSelector(string id)
        {
            var viewModel = new ProductDetailSelectorViewModel();
            var availableCoatings = viewModel.Colors.ToList();
            var content = _umbracoHelper.Content(id);
            if (content is Product product)
            {
                var coatingInfos = product.Colors ?? Enumerable.Empty<ProductColor>();
                viewModel.Sizes = product.AvailableSizes.Any() ? product.AvailableSizes : Enumerable.Empty<string>();
                foreach (var coating in coatingInfos)
                {
                    var colorInfo = _umbracoHelper.Content<Colour>(coating?.AvailableColor?.Id.ToString());
                    if (colorInfo == null)
                        continue;

                    var coatingInfo = new ColorModel
                    {
                        ColorName = colorInfo.ColourName,
                        ColorImageUrl = colorInfo.GetCropImageUrl(ImageCropProfile.ColorImage),
                        ColorUid = Guid.NewGuid().ToString()
                    };

                    var availableTimbers = new List<TimberModel>();

                    foreach (var timber in coating.AvailableTimbers)
                    {
                        var timberContent = _umbracoHelper.Content<Timber>(timber?.TimberName?.Id.ToString());
                        if (timberContent == null)
                            continue;

                        var rawOption = GetRawCoat(timberContent);
                        var availableCoats = new List<CoatModel>();
                        var timberCoats = timber.Coats.Where(x => !string.IsNullOrEmpty(x.CoatOption) && x.CoatImage is Image);
                        if (!timberCoats.Any(x => x.CoatOption.Equals("Raw", StringComparison.OrdinalIgnoreCase)) && timberContent.TimberImage is IPublishedContent)
                        {
                            availableCoats.Add(rawOption);
                        }

                        foreach (var coat in timberCoats)
                        {
                            availableCoats.Add(new CoatModel
                            {
                                CoatOption = coat.CoatOption,
                                CoatImageUrl = coat.CoatImage.GetCropImageUrl(ImageCropProfile.CoatImage)
                            });
                        }
                        if(!availableCoats.Any())
                            continue;

                        var timberViewModel = new TimberModel
                        {
                            TimberName = string.IsNullOrEmpty(timberContent.TimberName) ? timberContent.Name : timberContent.TimberName,
                            TimberUid = Guid.NewGuid().ToString(),
                            Coats = availableCoats
                        };
                        availableTimbers.Add(timberViewModel);
                    }
                    coatingInfo.Timbers = availableTimbers;
                    availableCoatings.Add(coatingInfo);
                }

                viewModel.TimberCaption = product.TimberCaption;
                viewModel.ColorCaption = product.ColorCaption;
                viewModel.Title = product.PageTitle;
                viewModel.ShopifyMappings = product.ShopifyMapping.Select(
                                                 s => new ShopifyMapping { 
                                                     SizeColor = s.Sizes + "-" + (s.Colors == null ? string.Empty : s.Colors.Name),
                                                     SKU = s.ShopifyValue
                                                 }
                                            ).ToList();
            }
            viewModel.Colors = availableCoatings;
            return viewModel;
        }

        [HttpPost]
        public ProductDetailWishListViewModel GetProductDetailInfo([FromBody] WishListItemInfosRequest wishListItemRequest, [FromUri] string viewMode)
        {
            var content = _umbracoHelper.Content(wishListItemRequest.Id);
            var imageCropProfile = GetImageCropProfileByViewMode(viewMode);
            return GetProductDetailWishListViewModel(wishListItemRequest, content, imageCropProfile);
        }

        [HttpPost]
        public IEnumerable<ProductDetailWishListViewModel> GetProductDetailsByIds([FromBody] IEnumerable<WishListItemInfosRequest> wishListItemsRequest, [FromUri] string viewMode)
        {
            if (wishListItemsRequest == null || !wishListItemsRequest.Any())
                return Enumerable.Empty<ProductDetailWishListViewModel>();

            var contentInfos = _umbracoHelper.Content(wishListItemsRequest.Select(x => x.Id));
            var validProductDetailWishListViewModels = new List<ProductDetailWishListViewModel>();
            var imageCropProfile = GetImageCropProfileByViewMode(viewMode);

            foreach (var wishListItem in wishListItemsRequest)
            {
                var content = contentInfos.FirstOrDefault(x => x.Id.ToString().Equals(wishListItem.Id, StringComparison.OrdinalIgnoreCase));
                validProductDetailWishListViewModels.Add(GetProductDetailWishListViewModel(wishListItem, content, imageCropProfile));
            }

            return validProductDetailWishListViewModels.Where(x => x.IsValidProduct);
        }

        private ImageCropProfile GetImageCropProfileByViewMode(string viewMode)
        {
            if (viewMode.Equals("WishListDetails", StringComparison.OrdinalIgnoreCase))
                return ImageCropProfile.WishListProductDetailsImage;

            return ImageCropProfile.WishListProductImage;
        }

        private ProductDetailWishListViewModel GetProductDetailWishListViewModel(WishListItemInfosRequest wishListItemRequest, IPublishedContent content, ImageCropProfile imageCropProfile)
        {
            var viewModel = new ProductDetailWishListViewModel();
            if (string.IsNullOrEmpty(wishListItemRequest.Size))
                return viewModel;

            if (content is Product product)
            {
                viewModel.Id = $"{wishListItemRequest.Id}-{wishListItemRequest.Color}-{wishListItemRequest.Size}";

                if (product.AvailableSizes == null || !product.AvailableSizes.Any() || !product.AvailableSizes.Contains(wishListItemRequest.Size, StringComparer.OrdinalIgnoreCase))
                    return viewModel;

                foreach (var productColor in product.Colors)
                {
                    var colorInfo = _umbracoHelper.Content<Colour>(productColor.AvailableColor.Id.ToString());
                    if (colorInfo != null && colorInfo.ColourName.Equals(wishListItemRequest.Color, StringComparison.OrdinalIgnoreCase))
                    {
                        viewModel.ColorName = colorInfo.ColourName;
                        viewModel.ColorImageUrl = colorInfo.GetCropImageUrl(ImageCropProfile.WishListColorImage).ToAbsoluteUrl();
                        break;
                    }
                }

                viewModel.Size = product.AvailableSizes.First(x => x.Equals(wishListItemRequest.Size, StringComparison.OrdinalIgnoreCase));

                if (product.Images.Any())
                {
                    var images = _mapper.Map<IEnumerable<ImageModel>>(product.Images);
                    var productImage = images?.FirstOrDefault(x => x.ProductImageSize.Equals(wishListItemRequest.Size, StringComparison.OrdinalIgnoreCase)) ?? images.FirstOrDefault();
                    viewModel.ProductImageUrl = productImage != null ? productImage.Url.GetCropImageUrl(107, 121, imageCropProfile).ToAbsoluteUrl() : string.Empty;
                    viewModel.ProductImageAlt = productImage?.Name ?? string.Empty;
                }

                viewModel.Title = string.IsNullOrEmpty(product.ListingTitle) ? product.PageTitle : product.ListingTitle;

                if (!string.IsNullOrEmpty(viewModel.ColorName))
                {
                    viewModel.ProductUri = product.Url.SetQueryParam("selectedColor", viewModel.ColorName.ToLower()).SetQueryParam("selectedSize", viewModel.Size.ToLower()).ToString(true);
                }
                else
                {
                    viewModel.ProductUri = product.Url.SetQueryParam("selectedSize", viewModel.Size.ToLower()).ToString(true);
                }

                viewModel.Description = !string.IsNullOrEmpty(product.SecondaryTitle) ? product.SecondaryTitle : string.Empty;
                viewModel.IsValidProduct = true;
                viewModel.SKU = product.ShopifyMapping.SingleOrDefault(m => m.Sizes.Equals(wishListItemRequest.Size, StringComparison.OrdinalIgnoreCase)
                                                                     && ((m.Colors == null && string.IsNullOrEmpty(wishListItemRequest.Color))
                                                                         || (m.Colors != null && m.Colors.Name.Equals(wishListItemRequest.Color, StringComparison.OrdinalIgnoreCase)))
                                                                       )
                                                     ?.ShopifyValue;                                                     
            }

            return viewModel;
        }
    }
}