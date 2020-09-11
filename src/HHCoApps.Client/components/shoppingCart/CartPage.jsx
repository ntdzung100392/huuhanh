import React from "react";
import { CUSTOM_EVENT_NAME } from 'constants/AppConstants';
import { ShopifyContext } from 'services/shopify-context';
import LineItemContent from './LineItemContent';

class CartPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      enable: false,
      checkout: {}
    };
    this.addToShoppingCart = this.addToShoppingCart.bind(this);
    this.completeCheckout = this.completeCheckout.bind(this);
    this.onChangeQuantity = this.onChangeQuantity.bind(this);
    this.onRemoveItem = this.onRemoveItem.bind(this);
    this.renderRow = this.renderRow.bind(this);
    this.show = this.show.bind(this);
    this.hide = this.hide.bind(this);
  }

  componentDidMount() {
    document.addEventListener(CUSTOM_EVENT_NAME.addToShoppingCart, this.addToShoppingCart);
    document.addEventListener(CUSTOM_EVENT_NAME.showCartPage, this.show);

    if (this.context.cart) {
      this.context.cart.getCheckout().then((checkoutData) => {
        this.setState({
          ...this.state,
          checkout: checkoutData
        });
      }, () => {
        this.setState({
          ...this.state,
          checkout: {}
        });
      });
    }
  }

  addToShoppingCart(event) {
    const variant = event.detail;

    if (this.context.cart && variant) {
      this.context.cart.addToCart(variant.id);
    }
  }

  completeCheckout() {
    if (this.context.cart) {
      this.context.cart.completeCheckout().then(checkout => {
        window.open(checkout.webUrl, '_blank');
      }, (error) => {
        console.log(error);
        this.setState({
          ...this.state,
          checkout: {}
        });
      });
    }
  }

  show() {
    this.setState({
      ...this.state,
      enable: true
    })
  }

  hide(event) {
    console.log(event);
    event.stopPropagation();
    event.preventDefault();

    this.setState({
      ...this.state,
      enable: false
    })
  }

  renderRow() {
    const {checkout} = this.state;
    const lineItems = checkout.lineItems || {};

    if (lineItems && lineItems.length > 0) {
      return lineItems.map(item => {
        return <LineItemContent key={item.id} item={item} onRemoveItem={this.onRemoveItem} onChangeQuantity={this.onChangeQuantity}/>
      })
    } else {
      return (
        <div className="empty-cart">There is no item in Cart</div>
      )
    }
  }

  onChangeQuantity(product, isDecrease) {
    console.log(product, isDecrease);
  }

  onRemoveItem(product) {
    console.log(product);
  }

  render() {
    const {checkout, enable} = this.state;
    const lineItems = checkout.lineItems || [];
    return (
      <React.Fragment>
        {
          enable && <div className='cart-page-backdrop' onClick={(event) => this.hide(event)}></div>
        }

        <div className="shopping-cart-page" aria-hidden={!enable}>
          <div className="cart-page-header">
            <div className="sidebar-cart__title">
              <p>Shopping Cart <span>({lineItems.reduce((total, item) => total + (item.quantity || 0), 0)})</span></p>
              <button id="close-wish-list"><span className="icon-close" onClick={this.hide}></span></button>
            </div>
            <p className="sidebar-cart__title--text">Create a shopping cart and quickly checkout it with shopify system. Here’s what you've chosen.</p>
          </div>
          <div className="cart-page-content">
            {this.renderRow()}
          </div>
          {
            checkout.webUrl &&
            <div className="cart-page-footer sidebar-cart__footer">
              <button className="btn btn-default complete-checkout" onClick={this.completeCheckout}>Checkout</button>
            </div>
          }
        </div>
      </React.Fragment>
    )
  }
}

CartPage.contextType = ShopifyContext;

export default CartPage;