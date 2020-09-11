import React from 'react';
import { MINI_WISHLIST_ITEM, EMAIL_FORM_ERROR_FIELDS } from 'constants/EntityTypes';
import { Button } from 'react-bootstrap';
import { VIEW_MODE } from 'constants/wishListPage/WishListConstant';
import { MONEY_FORMAT } from 'constants/AppConstants';
import WishListContentRow from './WishListContentRow';
import PropTypes from 'prop-types';
import SendWishListForm from './SendWishListForm';
import * as numeral from 'numeral';

class WishListContent extends React.Component {
  constructor(props) {
    super(props);

    this.renderRow = this.renderRow.bind(this);
    this.onChangeQuantity = this.onChangeQuantity.bind(this);
    this.onRemoveItem = this.onRemoveItem.bind(this);
    this.renderMiniWishList = this.renderMiniWishList.bind(this);
    this.renderWishListDetails = this.renderWishListDetails.bind(this);
  }

  renderRow() {
    return this.props.items.map(item => <WishListContentRow
      key={item.id}
      onChangeQuantity={this.props.onChangeQuantity}
      onRemoveItem={this.onRemoveItem}
      item={item}
      viewMode={this.props.viewMode}
      enableShopping = {this.props.enableShopping}
    />);
  }

  onChangeQuantity(product, isDecrease) {
    if (product) {
      this.props.onChangeQuantity(product, isDecrease);
    }
  }

  onRemoveItem(product) {
    if (product) {
      this.props.onRemoveItem(product);
    }
  }

  renderMiniWishList() {
    const enableShoppingFeature = !!this.props.enableShopping;
    const pageTitle = enableShoppingFeature ? 'Cart' : 'Wish List';
    const emptyMessage = enableShoppingFeature ? 'Your Cart is Empty' : 'Your Wish List is Empty';
    const pageDescription = enableShoppingFeature ? 'You can buy now with checkout button or quickly send it to yourself or a friend. Here’s what you\'ve chosen.': 'Create a wish list and quickly send it to yourself or a friend. Here’s what you\'ve chosen.';
    return (
      <React.Fragment>
        <div className="page-overlay"></div>
        <div id="sidebar-cart" className="sidebar-cart sidebar-cart--from-right" aria-hidden="true">
          {
            (this.props.checkoutInProgress) &&
            <div className="checkout-loading">
              <div className="overlay"></div>
              <div className="spinner-border">
                <span className="sr-only">Loading...</span>
              </div>
            </div>
          }
          <div className="sidebar-cart__header">
            <div className="sidebar-cart__title">
              <p>{pageTitle} <span>({this.props.items.reduce((total, item) => total + item.quantity, 0)})</span></p>
              <button id="close-wish-list"><span className="icon-close"></span></button>
            </div>
            <p className="sidebar-cart__title--text">{pageDescription}</p>
          </div>
          {this.props.items.length > 0 ?
            <form className="Card sidebar-cart__content">
              <div className="sidebar-cart__container">
                {this.renderRow()}
                {
                  enableShoppingFeature &&
                  <div className="payment-information">
                    <p>
                      <span className="shipping-rate-label">Metro</span> shipping rate
                      <span className="shipping-rate-value">
                        {numeral(AppConfig.shopify.shippingRateMetro || 0).format(MONEY_FORMAT)}
                      </span>
                    </p>
                    <p>
                      <span className="shipping-rate-label">Regional</span> shipping rate
                      <span className="shipping-rate-value">
                        {numeral(AppConfig.shopify.shippingRateRegional || 0).format(MONEY_FORMAT)}
                        </span>
                    </p>
                    <p>Shipping rate will be calculated at Checkout</p>
                  </div>
                }
              </div>
              <div className="sidebar-cart__footer">
                {
                  enableShoppingFeature &&
                  <React.Fragment>
                    <p>Subtotal
                      <span className="subtotal-amount">
                        {numeral(this.props.items.reduce((total, item) => total + (item.quantity * item.variant.price), 0)).format(MONEY_FORMAT)}
                      </span>
                    </p>
                    <Button className="btn checkout-btn" onClick={this.props.onCheckout}>
                      <span>Process to Checkout</span>
                    </Button>
                  </React.Fragment>
                }
                <Button className="btn" onClick={this.props.onClickSendWishList}>
                  <span>Send Wish List</span>
                </Button>
              </div>
            </form>
            :
            <form className="Card sidebar-cart__content">
              <div className="sidebar-cart__main">
                <p className="cart-item__empty">{emptyMessage}</p>
              </div>
            </form>
          }
        </div>
      </React.Fragment>
    );
  }

  renderWishListDetails() {
    return (
      <React.Fragment>
        <div className="wish-list-page">
          <div className="container">
            <div className="wish-list">
              <div className="wish-list__table">
                <table className="table table-borderless">
                  <thead>
                    <tr>
                      <th className="header-product">Product</th>
                      <th>Colour</th>
                      <th>Size</th>
                      <th>Quantity</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.items.length > 0 ? this.renderRow() :
                      <tr>
                        <td colSpan="4">
                          <div className="cart-table__colour">
                            <span className="bold">Your Wish List is Empty</span>
                          </div>
                        </td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
              <SendWishListForm
                itemsQuantity={(this.props.items.length > 0) ? (this.props.items.reduce((total, item) => total + item.quantity, 0)) : 0}
                onChangeValue={this.props.onChangeValue}
                onSubmitEmailRequest={this.props.onSubmitEmailRequest}
                firstName={this.props.firstName}
                lastName={this.props.lastName}
                email={this.props.email}
                fieldErrors={this.props.fieldErrors}
                errorMessage={this.props.errorMessage}
                isLoading={this.props.isLoading}
              />
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  }

  render() {
    switch (this.props.viewMode) {
      case VIEW_MODE.miniWishList:
        return this.renderMiniWishList();
      case VIEW_MODE.wishListDetails:
        return this.renderWishListDetails();
      default:
        return this.renderMiniWishList();
    }
  }
}

WishListContent.propTypes = {
  items: PropTypes.arrayOf(MINI_WISHLIST_ITEM),
  viewMode: PropTypes.string,
  onClickSendWishList: PropTypes.func,
  onChangeQuantity: PropTypes.func,
  onRemoveItem: PropTypes.func,
  firstName: PropTypes.string,
  lastName: PropTypes.string,
  email: PropTypes.string,
  isLoading: PropTypes.bool,
  fieldErrors: EMAIL_FORM_ERROR_FIELDS,
  errorMessage: PropTypes.string,
  onChangeValue: PropTypes.func,
  onSubmitEmailRequest: PropTypes.func,
  onCheckout: PropTypes.func,
  enableShopping: PropTypes.bool,
  checkoutInProgress: PropTypes.bool
};

export default WishListContent;