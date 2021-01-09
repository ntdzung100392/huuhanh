using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HHCoApps.CMSWeb.Helpers.CdnUrlResolvers;
using HHCoApps.CMSWeb.Helpers.Enum;
using HHCoApps.CMSWeb.Models;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class UrlExtensions
    {
        private static ICdnUrlResolver _cdnResolver;
        public const string AcceptTypeImageWebP = "image/webp";

        private static ICdnUrlResolver CdnResolver
        {
            get { return _cdnResolver ??= Current.Factory.GetInstance<ICdnUrlResolver>(); }
        }

        public static string GetCropImageUrl(this UrlHelper urlHelper, string imageUrl, ImageCropProfile cropProfile)
        {
            if (urlHelper == null)
                throw new ArgumentNullException(nameof(urlHelper));

            if (string.IsNullOrEmpty(imageUrl))
                return GetNoImageUrl(cropProfile);

            if (cropProfile.Disable)
                return imageUrl;

            var croppedImageUrl = GetImageCropUrl(imageUrl, cropProfile);
            return CdnResolver.GetCdnUrl(croppedImageUrl);
        }

        public static string GetCropImageUrl(this IPublishedContent imageContent, ImageCropProfile cropProfile)
        {
            if (imageContent == null)
                return GetNoImageUrl(cropProfile);

            if (imageContent is Image image)
                return image.Url.GetCropImageUrl(image.UmbracoWidth, image.UmbracoHeight, cropProfile);

            var contentInfo = Current.Factory.GetInstance<IMapper>().Map<ContentInfoModel>(imageContent);
            var croppedImageUrl = contentInfo.ImageUrl.GetCropImageUrl(contentInfo.ImageWidth, contentInfo.ImageHeight, cropProfile);

            return CdnResolver.GetCdnUrl(croppedImageUrl);
        }

        public static string GetCropImageUrl(this string imageUrl, int width, int height, ImageCropProfile cropProfile)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return GetNoImageUrl(cropProfile);

            if (cropProfile.Disable)
                return imageUrl;

            var cropRatioMode = width > height ? ImageCropRatioMode.Width : ImageCropRatioMode.Height;
            var isImageSmaller = height < cropProfile.Height && width < cropProfile.Width;
            var imageCropMode = cropProfile.ImageCropMode ?? (isImageSmaller ? ImageCropMode.Crop : ImageCropMode.BoxPad);

            var furtherOptions = GetFurtherOptions(cropProfile, imageCropMode);
            var resizeToWidth  = width > cropProfile.MaxWidth ? cropProfile.MaxWidth : cropProfile.Width;

            var croppedImageUrl = imageUrl.GetCropUrl(
                resizeToWidth,
                cropProfile.Height,
                imageCropMode: imageCropMode,
                ratioMode: cropRatioMode,
                furtherOptions: furtherOptions,
                imageCropAnchor: ImageCropAnchor.Center);

            return CdnResolver.GetCdnUrl(croppedImageUrl);
        }

        public static string ToAbsoluteUrl(this string relativeUrl, Uri requestUri = null)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null && requestUri == null)
                return relativeUrl;

            if (Uri.TryCreate(relativeUrl, UriKind.Absolute, out var absoluteUrl))
                return absoluteUrl.ToString();

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            requestUri ??= HttpContext.Current.Request.Url;
            var port = requestUri.Port != 80 ? (":" + requestUri.Port) : String.Empty;

            return $"{requestUri.Scheme}://{requestUri.Host}{port}{VirtualPathUtility.ToAbsolute(relativeUrl)}";
        }

        private static string GetImageCropUrl(string imageUrl, ImageCropProfile cropProfile)
        {
            var cropRatioMode = GetCropRatioMode(cropProfile);
            var cropMode = cropProfile.ImageCropMode ?? ImageCropMode.Crop;
            var furtherOptions = GetFurtherOptions(cropProfile, cropMode);

            return imageUrl.GetCropUrl(
                cropProfile.Width,
                cropProfile.Height,
                imageCropMode: cropMode,
                ratioMode: cropRatioMode,
                furtherOptions: furtherOptions,
                imageCropAnchor: ImageCropAnchor.Center);
        }

        private static ImageCropRatioMode? GetCropRatioMode(ImageCropProfile cropProfile)
        {
            if (cropProfile.Height.HasValue && cropProfile.Width.HasValue)
            {
                return cropProfile.Width > cropProfile.Height ? ImageCropRatioMode.Width : ImageCropRatioMode.Height;
            }

            return null;
        }

        private static string GetNoImageUrl(ImageCropProfile cropProfile)
        {
            return "/images/no-image.jpg".GetCropUrl(width: cropProfile.Width, height: cropProfile.Height, imageCropMode: ImageCropMode.Stretch);
        }

        private static string GetFurtherOptions(ImageCropProfile cropProfile, ImageCropMode imageCropMode)
        {
            var furtherOptions = string.Empty;
            if (imageCropMode == ImageCropMode.BoxPad && cropProfile.UseBackgroundColor)
            {
                furtherOptions = "&bgcolor=fff";
            }

            if (IsRequestSupportWebP() && cropProfile.IsRenderWebp)
            {
                furtherOptions += "&format=webp";
            }

            return furtherOptions;
        }

        private static bool IsRequestSupportWebP()
        {
            return (HttpContext.Current.Request.AcceptTypes ?? Array.Empty<string>())
                .Any(x => x.Equals(AcceptTypeImageWebP, StringComparison.OrdinalIgnoreCase));
        }

        public static string GetLastPartUrl(this string url)
        {
            return url.Trim('/').Split('/').Last();
        }
    }
}