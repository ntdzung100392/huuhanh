import "core-js/stable";
import React from 'react';
import ReactDOM from 'react-dom';
import SearchPageContainer from 'components/SearchPage/SearchPageContainer';
import QuestionsPageContainer from 'components/QuestionsPage/QuestionsPageContainer';
import ResponsiveItemListingPageContainer from 'components/ResponsiveItemListingPage/ResponsiveItemListingPageContainer';
import StoreLocatorContainer from 'components/StoreLocatorPage/StoreLocatorContainer';
import ProductDetailSelectorContainer from 'components/productDetailSelector/ProductDetailSelectorContainer';
import WishListContainer from 'components/wishListComponent/WishListContainer';
import ProductNavigationContainer from 'components/productNavigation/productNavigationContainer';
import ShoppingCart from 'components/shoppingCart/ShoppingCart';
import CartPage from "components/shoppingCart/CartPage";

import { ShopifyContext } from 'services/shopify-context';
import ShopifyClient, { ShopifyCart } from 'services/shopify-client';


const rootElements = document.getElementsByClassName("react-app-root");
for (var i = 0; i < rootElements.length; i++) {
  const rootElement = rootElements[i];
  const page = rootElement.getAttribute('data-page');
  const contentId = rootElement.getAttribute('data-content-id');
  const productTitle = rootElement.getAttribute('data-product-title');

  const App = () => {
    switch (page) {
      case 'QuestionsPage':
        return <QuestionsPageContainer categoryId={contentId} />;
      case 'ResponsiveItemListingPage':
        const itemsPerPage = parseInt(rootElement.getAttribute('data-item-per-row'), 10);
        const filterBy = JSON.parse(rootElement.getAttribute('data-filtering-by'));
        const isGroupBySubCategory = rootElement.getAttribute('data-isgroupby-subcategory') === 'True';
        const itemView = rootElement.getAttribute('data-template-name');
        const enabledFilter = rootElement.getAttribute('data-enabled-filtering') === 'True';
        const primaryFilterIds = JSON.parse(rootElement.getAttribute('data-primary-filter-ids'));
        const includedContentIds = JSON.parse(rootElement.getAttribute('data-included-items'));
        const sortBy = rootElement.getAttribute('data-sort-by');
        const orderType = rootElement.getAttribute('data-order-type');
        const contentType = rootElement.getAttribute('data-content-type');
        const defaultFilteringBy = JSON.parse(rootElement.getAttribute('data-default-filter'));
        const groupByFilter = rootElement.getAttribute('data-group-by-filter');
        const viewMoreLabel = rootElement.getAttribute('data-view-more-label');
        const viewDetailsLabel = rootElement.getAttribute('data-view-details-label');
        return <ResponsiveItemListingPageContainer
          categoryId={contentId}
          itemsPerPage={itemsPerPage}
          isGroupBySubCategory={isGroupBySubCategory}
          itemView={itemView}
          enabledFilter={enabledFilter}
          filterBy={filterBy}
          includedContentIds={includedContentIds}
          sortBy={sortBy}
          orderType={orderType}
          contentType={contentType}
          primaryFilterIds={primaryFilterIds}
          defaultFilteringBy={defaultFilteringBy}
          viewMoreLabel={viewMoreLabel}
          viewDetailsLabel={viewDetailsLabel}
          groupByFilter={groupByFilter} />;
      case 'StoreLocatorPage':
        return <StoreLocatorContainer contentId={contentId} />;
      case 'ProductDetailPage':
        return <ProductDetailSelectorContainer contentId={contentId} productTitle={productTitle} />;
      case 'WishListPage':
        const viewMode = rootElement.getAttribute('data-view-mode');
        return <WishListContainer viewMode={viewMode} />;
      case 'ProductNavigationPage':
        return <ProductNavigationContainer contentId={contentId} />;
      case 'ShoppingCart':
        return <ShoppingCart contentId={contentId} />;
      case 'CartPage':
        return <CartPage contentId={contentId} />;
      case 'SearchPage':
      default:
        const productsPath = rootElement.getAttribute('data-products-path');
        const blogsPath = rootElement.getAttribute('data-blogs-path');
        const otherNodePaths = JSON.parse(rootElement.getAttribute('data-others-paths'));
        return <SearchPageContainer productsPath={productsPath} blogsPath={blogsPath} otherNodePaths={otherNodePaths} />;
    }
  };

  const Page = () => {
    let context = {};

    if (AppConfig.shopify && AppConfig.shopify.enable) {
      const shopify = new ShopifyClient({
        domain: AppConfig.shopify.domain,
        token: AppConfig.shopify.token
      });

      context = {
        client: shopify,
        cart: new ShopifyCart(shopify.client.checkout)
      };
    }


    return (
        <ShopifyContext.Provider value={context}>
          <App/>
        </ShopifyContext.Provider>
    )
  };

  ReactDOM.render(<Page />, rootElement);
}