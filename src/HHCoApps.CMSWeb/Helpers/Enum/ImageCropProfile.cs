using HHCoApps.CMSWeb.Headspring;
using Umbraco.Web.Models;

namespace HHCoApps.CMSWeb.Helpers.Enum
{
    public class ImageCropProfile : Enumeration<ImageCropProfile, string>
    {
        public static ImageCropProfile Carousel = new ImageCropProfile(nameof(Carousel), nameof(Carousel)) { Width = 1200, Height = 540 };
        public static ImageCropProfile CarouselMedium = new ImageCropProfile(nameof(CarouselMedium), nameof(CarouselMedium)) { Width = 1200, Height = 540 };
        public static ImageCropProfile CarouselLarge = new ImageCropProfile(nameof(CarouselLarge), nameof(CarouselLarge)) { Width = 1440, Height = 720 };
        public static ImageCropProfile CarouselOneColumn = new ImageCropProfile(nameof(CarouselOneColumn), nameof(CarouselOneColumn)) { Width = 719, Height = 807 };

        public static ImageCropProfile BannerImage = new ImageCropProfile(nameof(BannerImage), nameof(BannerImage)) { Width = 718, Height = 441 };
        public static ImageCropProfile HeaderBannerImage = new ImageCropProfile(nameof(HeaderBannerImage), nameof(HeaderBannerImage)) { Width = 1920, Height = 360, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop, IsRenderWebp = false };
        public static ImageCropProfile FooterBannerImage = new ImageCropProfile(nameof(FooterBannerImage), nameof(FooterBannerImage)) { Width = 1920, Height = 360, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop, IsRenderWebp = false };
        public static ImageCropProfile EmailHeaderBannerImage = new ImageCropProfile(nameof(EmailHeaderBannerImage), nameof(EmailHeaderBannerImage)) { Width = 600, Height = 440, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile EmailFooterBannerImage = new ImageCropProfile(nameof(EmailFooterBannerImage), nameof(EmailFooterBannerImage)) { Width = 600, Height = 300, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile EmailHeaderBannerLogo = new ImageCropProfile(nameof(EmailHeaderBannerLogo), nameof(EmailHeaderBannerLogo)) { Width = 600, Height = 146, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };

        public static ImageCropProfile ThreeColumnBlog = new ImageCropProfile(nameof(ThreeColumnBlog), nameof(ThreeColumnBlog)) { Width = 480, Height = 356, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile ThreeColumnsWithFlexibleHeightItems = new ImageCropProfile(nameof(ThreeColumnsWithFlexibleHeightItems), nameof(ThreeColumnsWithFlexibleHeightItems)) { Width = 328, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile OneColumnSlider = new ImageCropProfile(nameof(OneColumnSlider), nameof(OneColumnSlider)) { MaxWidth = 470, Height = 303, UseBackgroundColor = false, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile TwoColumn = new ImageCropProfile(nameof(TwoColumn), nameof(TwoColumn)) { Width = 538, Height = 380, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile FourColumn = new ImageCropProfile(nameof(FourColumn), nameof(FourColumn)) { Width = 215, Height = 241 };

        public static ImageCropProfile Banner = new ImageCropProfile(nameof(Banner), nameof(Banner)) { Width = 638, Height = 626, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Min };
        public static ImageCropProfile ProductImage = new ImageCropProfile(nameof(ProductImage), nameof(ProductImage)) { Width = 291, Height = 300, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad };
        public static ImageCropProfile ProductImageThumbnail = new ImageCropProfile(nameof(ProductImageThumbnail), nameof(ProductImageThumbnail)) { Width = 45, Height = 54 };
        public static ImageCropProfile CommonProblemImageThumbnail = new ImageCropProfile(nameof(CommonProblemImageThumbnail), nameof(CommonProblemImageThumbnail)) { Width = 380, Height = 220 };

        public static ImageCropProfile IconLink = new ImageCropProfile(nameof(IconLink), nameof(IconLink)) { Width = 36, Height = 36 };
        public static ImageCropProfile Logo = new ImageCropProfile(nameof(Logo), nameof(Logo)) { Width = 225, Height = 31, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad, UseBackgroundColor = false, IsRenderWebp = false };
        public static ImageCropProfile FooterLogo = new ImageCropProfile(nameof(FooterLogo), nameof(FooterLogo)) { Width = 325, Height = 131, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad, UseBackgroundColor = false, IsRenderWebp = false };
        public static ImageCropProfile Gallery = new ImageCropProfile(nameof(Gallery), nameof(Gallery)) { Height = 246, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Pad };

        public static ImageCropProfile ThreeColumnsWithCaption = new ImageCropProfile(nameof(ThreeColumnsWithCaption), nameof(ThreeColumnsWithCaption)) { Width = 356, Height = 284, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile ThreeColumnSlider = new ImageCropProfile(nameof(ThreeColumnSlider), nameof(ThreeColumnSlider)) { Width = 200, Height = 228, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad };
        public static ImageCropProfile ThreeColumnFull = new ImageCropProfile(nameof(ThreeColumnFull), nameof(ThreeColumnFull)) { Width = 440, Height = 300, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile ThreeColumnSliderCenterAligned = new ImageCropProfile(nameof(ThreeColumnSliderCenterAligned), nameof(ThreeColumnSliderCenterAligned)) { Width = 440, Height = 284, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad };

        public static ImageCropProfile ThreeColumnLarge = new ImageCropProfile(nameof(ThreeColumnLarge), nameof(ThreeColumnLarge)) { Width = 800, Height = 512, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad };

        public static ImageCropProfile ColorImage = new ImageCropProfile(nameof(ColorImage), nameof(ColorImage)) { Width = 60, Height = 60, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile CoatImage = new ImageCropProfile(nameof(CoatImage), nameof(CoatImage)) { Width = 135, Height = 93, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };

        public static ImageCropProfile WishListProductImage = new ImageCropProfile(nameof(WishListProductImage), nameof(WishListProductImage)) { Width = 107, Height = 121, ImageCropMode = Umbraco.Web.Models.ImageCropMode.BoxPad };
        public static ImageCropProfile WishListProductDetailsImage = new ImageCropProfile(nameof(WishListProductDetailsImage), nameof(WishListProductDetailsImage)) { Width = 107, Height = 121, UseBackgroundColor = false, ImageCropMode = Umbraco.Web.Models.ImageCropMode.Crop };
        public static ImageCropProfile WishListColorImage = new ImageCropProfile(nameof(WishListColorImage), nameof(WishListColorImage)) { Width = 25, Height = 20, UseBackgroundColor = false };

        public ImageCropProfile(string value, string displayName) : base(value, displayName)
        {
        }

        public int? Width { get; set; }
        public int? MaxWidth { get; set; }
        public int? Height { get; set; }
        public bool Disable { get; set; }
        public ImageCropMode? ImageCropMode { get; set; }
        public bool UseBackgroundColor { get; set; } = true;
        public bool IsRenderWebp { get; set; } = true;
    }
}