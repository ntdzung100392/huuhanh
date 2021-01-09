using HHCoApps.CMSWeb.Services;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers
{
    public class ServiceComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IItemListingService, ItemListingService>();
            composition.Register<IBannerService, BannerService>();
            composition.Register<IToolAndTipService, ToolAndTipService>();
            composition.Register<IContentIndexQueryService, ContentIndexQueryService>();
            composition.Register<IFilterCategoryService, FilterCategoryService>();
            composition.Register<IMenuItemService, MenuItemService>();
            composition.Register<IProductDetailService, ProductDetailService>();
            composition.Register<ILargeTileService, LargeTileService>();
            composition.Register<IListingComponentService, ListingComponentService>();
            composition.Register<ISeoMetadataService, SeoMetadataService>();
            composition.Register<IGalleryComponentService, GalleryComponentService>();
            composition.Register<IContentInfoService, ContentInfoService>();
            composition.Register<INavigationService, NavigationService>();
            composition.Register<IFooterNavigationService, FooterNavigationService>();
            composition.Register<IRelatedProductService, RelatedProductService>();
            composition.Register<IEmailTemplateService, EmailTemplateService>();
        }
    }
}