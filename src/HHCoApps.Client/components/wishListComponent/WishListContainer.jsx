import React from 'react';
import PropTypes from 'prop-types';
import { STRING_RESOURCES } from 'constants/wishListPage/WishListConstant';
import ProductDetailSelectorApi from "apis/ProductDetailSelectorApi";
import WishListContent from "./WishListContent";
import WishListApi from "apis/WishListApi";
import StringHelper from "helpers/StringHelper";
import * as Storage from "services/storage";
import { ShopifyContext } from 'services/shopify-context';

class WishListContainer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      miniCartItems: [],
      firstName: '',
      lastName: '',
      email: '',
      fieldErrors: {
        firstName: '',
        lastName: '',
        email: '',
      },
      errorMessage: '',
      isLoading: false,
      checkout: {},
      checkoutInProgress: false
    };

    this.onClickSendWishList = this.onClickSendWishList.bind(this);
    this.onChangeQuantity = this.onChangeQuantity.bind(this);
    this.onRemoveItem = this.onRemoveItem.bind(this);
    this.setLocalStorage = this.setLocalStorage.bind(this);
    this.refreshMiniWishList = this.refreshMiniWishList.bind(this);
    this.updateNewWishItem = this.updateNewWishItem.bind(this);
    this.handleNewValidWishItem = this.handleNewValidWishItem.bind(this);
    this.refreshValidWishItems = this.refreshValidWishItems.bind(this);
    this.onChangeValue = this.onChangeValue.bind(this);
    this.onSubmitEmailRequest = this.onSubmitEmailRequest.bind(this);
    this.countErrors = this.countErrors.bind(this);
    this.validateError = this.validateError.bind(this);
    this.onCheckout = this.onCheckout.bind(this);
    this.trackingSendWishlist = this.trackingSendWishlist.bind(this);
  }

  componentDidMount() {
    document.addEventListener(STRING_RESOURCES.wishListUpdatedEvent, this.refreshMiniWishList);
    document.addEventListener(STRING_RESOURCES.newWishItemAdded, this.updateNewWishItem);

    const miniCartItems = JSON.parse(localStorage.getItem(STRING_RESOURCES.feastWatsonWishList));

    if (miniCartItems) {
      let wishListItemsRequest = [];
      miniCartItems.forEach(item => {
        const splitedItemId = item.id.split('-');
        if (splitedItemId.length === 3) {
          wishListItemsRequest.push({
            id: splitedItemId[0],
            color: splitedItemId[1],
            size: splitedItemId[2]
          });
        }
      });

      if (wishListItemsRequest) {
        ProductDetailSelectorApi.getProductDetailsByIds(wishListItemsRequest)
          .then(response => {
            this.refreshValidWishItems(response.data);
          });
      } else {
        this.setState({
          ...this.state,
          miniCartItems: []
        }, () => {
          this.setLocalStorage();
        });
      }
    } else {
      this.setState({
        ...this.state,
        miniCartItems: []
      });
    }
  }

  componentWillUnmount() {
    document.removeEventListener(STRING_RESOURCES.wishListUpdatedEvent, this.refreshMiniWishList);
  }

  updateNewWishItem(e) {
    if (!e.detail || !e.detail.wishItemId) {
      return;
    }

    const newItemValue = e.detail.wishItemId.split('-');

    if (newItemValue.length === 3) {
      const variantId = e.detail.variantId || '';
      const sku = e.detail.sku || '';
      const price = e.detail.price || '';

      ProductDetailSelectorApi.getProductDetailInfo({ id: newItemValue[0], color: newItemValue[1], size: newItemValue[2] }, this.props.viewMode)
        .then(response => {
          this.handleNewValidWishItem({...response.data, variant: {id: variantId, sku: sku, price: price}});
        });
    }
  }

  refreshValidWishItems(validItems) {
    const miniCartItems = JSON.parse(localStorage.getItem(STRING_RESOURCES.feastWatsonWishList));

    const validMiniCartItems = validItems.map(validItem => {
      const itemIndex = miniCartItems.findIndex(product => product.id === validItem.id);
      return {
        id: validItem.id,
        title: validItem.title,
        size: validItem.size,
        colorName: validItem.colorName,
        colorImageUrl: validItem.colorImageUrl,
        productUri: validItem.productUri,
        productImageUrl: validItem.productImageUrl,
        productImageAlt: validItem.productImageAlt,
        caption: validItem.description,
        quantity: itemIndex >= 0 ? miniCartItems[itemIndex].quantity : 1,
        sku: itemIndex >= 0 ? validItem.sku || miniCartItems[itemIndex].sku : ''
      }
    });

    if (this.context.client && this.context.cart) {
      const productTitles = validMiniCartItems.map(item => item.title);
      this.context.client.searchProductByTitle(productTitles, productTitles.length).then((shopifyProducts) => {
        const productVariants = shopifyProducts.reduce((acc, item) => {
          return acc.concat(item.variants);
        }, []);

        const validShopifyItems = validMiniCartItems.reduce((acc, item) => {
          const variant = productVariants.find(variant => variant.sku === item.sku);

          if (variant) {
            item.variant = variant;
            acc.push(item);
          }

          return acc;
        }, []);

        this.setState({
          ...this.state,
          miniCartItems: validShopifyItems
        }, () => {
          this.setLocalStorage();

          this.context.cart.getCheckout().then((checkoutData) => {
            this.setState({
              ...this.state,
              checkout: checkoutData
            })
          }, () => {
            this.setState({
              ...this.state,
              miniCartItems: [],
              checkout: {}
            }, () => {
              this.setLocalStorage();
            });
          });
        });
      });
    } else {
      this.setState({
        ...this.state,
        miniCartItems: validMiniCartItems
      }, () => {
        this.setLocalStorage();
      });
    }
  }

  handleNewValidWishItem(item) {
    if (this.isSegmentEnabled()) {
      this.sendCartDataToSegment(item);
    }

    let miniCartItems = JSON.parse(localStorage.getItem(STRING_RESOURCES.feastWatsonWishList));
    if (!miniCartItems) {
      miniCartItems = [];
    }

    const itemIndex = miniCartItems.findIndex(product => product.id === item.id);
    if (itemIndex >= 0) {
      if (item.isValidProduct) {
        miniCartItems[itemIndex].quantity = miniCartItems[itemIndex].quantity + 1;
      } else {
        miniCartItems.splice(itemIndex, 1);
      }
    } else {
      if (item.isValidProduct) {
        miniCartItems.push({
          id: item.id,
          title: item.title,
          size: item.size,
          colorName: item.colorName,
          colorImageUrl: item.colorImageUrl,
          productUri: item.productUri,
          productImageUrl: item.productImageUrl,
          productImageAlt: item.productImageAlt,
          caption: item.description,
          quantity: 1,
          sku: item.sku || '',
          variant: item.variant || {}
        });
      }
    }

    this.setState({
      ...this.state,
      miniCartItems: miniCartItems
    }, () => {
      this.setLocalStorage();
    });

    if (this.context.cart) {
      this.context.cart.addToCart(item.variant.id);
    }
  }

  sendCartDataToSegment(item) {
    const cart = this.getCartSegmentData(item);
    analytics.track(STRING_RESOURCES.addToCartEventName, cart);
  }

  getCartSegmentData(cartData) {
    return {
      'productName': cartData.title,
      'productColor': cartData.colorName,
      'productSize': cartData.size,
      'productQuantity': 1,
      'productPrice': cartData.variant ? cartData.variant.price : 0,
      'category': STRING_RESOURCES.addToCartCategoryName
    }
  }

  onClickSendWishList() {
    window.location.href = "/wish-list-details";
  }

  onChangeQuantity(product, isDecrease) {
    let products = [...this.state.miniCartItems];
    products.forEach((item, index) => {
      if (item.id === product.id) {
        if (isDecrease) {
          item.quantity = item.quantity - 1;

          if (item.quantity == 0) {
            products.splice(index, 1);
          }
        } else {
          if (item.quantity < 99) {
            item.quantity = item.quantity + 1;
          }
        }
      }
    });

    this.setState({
      ...this.state,
      miniCartItems: products
    }, () => {
      this.setLocalStorage();
    });
  }

  onRemoveItem(item) {
    let products = [...this.state.miniCartItems];
    const toBeRemovedItemIndex = products.indexOf(item);
    if (toBeRemovedItemIndex >= 0) {
      products.splice(toBeRemovedItemIndex, 1);

      this.setState({
        ...this.state,
        miniCartItems: products
      }, () => {
        this.setLocalStorage();
      });
    }
  }

  setLocalStorage() {
    const miniCartItems = [...this.state.miniCartItems];
    if (this.state.miniCartItems.length > 0) {
      localStorage.setItem(STRING_RESOURCES.feastWatsonWishList, JSON.stringify(miniCartItems));
    } else {
      localStorage.removeItem(STRING_RESOURCES.feastWatsonWishList);
    }

    if (miniCartItems) {
      const updatedQuantityItems = miniCartItems.reduce((total, item) => total + item.quantity, 0);
      document.dispatchEvent(new CustomEvent(STRING_RESOURCES.wishListUpdatedEvent, { 'detail': updatedQuantityItems }));
    } else {
      document.dispatchEvent(new CustomEvent(STRING_RESOURCES.wishListUpdatedEvent, { 'detail': 0 }));
    }
  }

  refreshMiniWishList(event) {
    if (this.isSegmentEnabled() && event.detail && event.detail.item) {
      this.sendCartDataToSegment(event.detail.item);
    }

    const miniCartItems = JSON.parse(localStorage.getItem(STRING_RESOURCES.feastWatsonWishList)) || [];
    this.setState({
      ...this.state,
      miniCartItems: miniCartItems
    }, () => {
      if (this.context.cart) {
        const checkout = Storage.get();
        checkout.variants = miniCartItems.map(item => {
          return {
            variantId: item.variant.id,
            quantity: item.quantity
          }
        });

        Storage.save(checkout);
      } else {
        Storage.remove();
      }
    });
  }

  onChangeValue(event) {
    const { name, value } = event.target;
    this.setState({ [name]: value }, () => {
      this.validateError();
    });
  }

  onSubmitEmailRequest(event) {
    event.preventDefault();
    this.validateError();

    if (this.countErrors(this.state.fieldErrors) <= 0) {
      this.setState({ isLoading: true });
      const $this = this;
      grecaptcha.ready(function () {
        grecaptcha.execute(AppConfig.googleRecaptchaKey.publicKey, { action: 'submit' }).then(function (token) {
          let wishList = {
            firstName: $this.state.firstName,
            lastName: $this.state.lastName,
            email: $this.state.email,
            wishListItems: $this.state.miniCartItems,
            reCaptchaToken: token
          }

          $this.trackingSendWishlist();
          WishListApi.sendWishListEmail(wishList)
            .then(response => {
              $this.setState({ isLoading: false });
              if (response.data.isSuccess) {
                window.location.href = '/send-email-success';
              } else {
                $this.setState({ errorMessage: response.data.message });
              }
            }).catch(err => {
              $this.setState({ isLoading: false });
              $this.setState({ errorMessage: STRING_RESOURCES.unknownErrorMessage });
            });
        });
      });
    }
  }

  countErrors(errors) {
    let count = 0;
    Object.values(errors).forEach(
      (val) => val.length > 0 && (count = count + 1)
    );
    return count;
  }

  validateError() {
    let errors = this.state.fieldErrors;
    errors.firstName = !this.state.firstName ? STRING_RESOURCES.errorFirstNameMessage : '';
    errors.lastName = !this.state.lastName ? STRING_RESOURCES.errorLastNameMessage : '';
    errors.email = !this.state.email || !StringHelper.ValidEmailRegex.test(this.state.email) ? STRING_RESOURCES.errorEmailMessage : '';
    this.setState({ fieldErrors: errors });
  }

  onCheckout() {
    if (this.context.cart) {
      this.setState({...this.state, checkoutInProgress: true});
      let windowReference = null;

      if (this.isSafari()) {
        windowReference = window.open('', '_blank');
      }

      this.context.cart.completeCheckout().then(checkout => {
        if (windowReference) {
          windowReference.location = checkout.webUrl;
        } else {
          windowReference = window.open('', '_blank');
          windowReference.location = checkout.webUrl;
        }

        this.setState({...this.state, checkoutInProgress: false});
      }, () => {
        this.setState({
          ...this.state,
          checkoutInProgress: false,
          miniCartItems: [],
          checkout: {}
        }, () => {
          this.setLocalStorage();
        });
      });
    }
  }

  trackingSendWishlist() {
    if (AppConfig.integrations.segmentEnabled) {
      const miniCartItems = this.state.miniCartItems;
      const totalValue = miniCartItems.length
          ? miniCartItems.reduce((total, item) => total + (item.quantity * (item.variant ? item.variant.price : 0)), 0)
          : 0;
      const productNames = miniCartItems.map((item) => {
        return {
          productName: item.title,
          size: item.size,
          colour: item.colorName,
          quantity: item.quantity,
        };
      });
      analytics.track("Send wishlist", {
        productList: productNames,
        totalValue: totalValue,
        category: 'Submission'
      });
    }
  }

  isSafari() {
    let safari = false;
    const ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf('safari') != -1) {
      if (ua.indexOf('chrome') > -1) {
        safari = false;
      } else {
        safari = true;
      }
    }

    return safari;
  }

  isSegmentEnabled() {
    return AppConfig.integrations.segmentEnabled;
  }

  render() {
    const shoppingEnable = !! this.context.client;

    return (
      <React.Fragment>
        <WishListContent
          items={this.state.miniCartItems}
          viewMode={this.props.viewMode}
          onClickSendWishList={this.onClickSendWishList}
          onChangeQuantity={this.onChangeQuantity}
          onRemoveItem={this.onRemoveItem}
          onChangeValue={this.onChangeValue}
          onSubmitEmailRequest={this.onSubmitEmailRequest}
          firstName={this.state.firstName}
          lastName={this.state.lastName}
          email={this.state.email}
          fieldErrors={this.state.fieldErrors}
          errorMessage={this.state.errorMessage}
          isLoading={this.state.isLoading}
          onCheckout={this.onCheckout}
          enableShopping={shoppingEnable}
          checkoutInProgress={this.state.checkoutInProgress}
        />
      </React.Fragment>
    );
  }
}

WishListContainer.propTypes = {
  viewMode: PropTypes.string
};

WishListContainer.contextType = ShopifyContext;

export default WishListContainer;