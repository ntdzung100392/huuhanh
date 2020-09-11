import React from "react";
import { ShopifyContext } from 'services/shopify-context';
import { CUSTOM_EVENT_NAME } from 'constants/AppConstants';

class ShoppingCart extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      checkoutId: '',
      variants: [],
      shopifyCheckout: {},
      isLoaded: false
    };

    this.showCartPage = this.showCartPage.bind(this);
  }

  componentDidMount() {
    if (this.context.cart) {
      this.context.cart.observable.subscribe((checkoutData) => {
        const totalAmount = (checkoutData.variants || []).reduce((acc, variant) => acc + (variant.quantity || 0), 0);

        this.setState({
          ...this.state,
          checkoutId: checkoutData.checkoutId,
          variants: checkoutData.variants || []
        });

        setTimeout(() => {
          document.dispatchEvent(new CustomEvent(CUSTOM_EVENT_NAME.shoppingCartUpdate, {"detail": totalAmount}))
        })
      });
    }
  }

  showCartPage() {
    document.dispatchEvent(new CustomEvent(CUSTOM_EVENT_NAME.showCartPage));
  }

  render() {
    const {variants} = this.state;
    const total = (variants || []).reduce((acc, item) => acc + item.quantity, 0);

    return (
      <span className="cart-container">
        <span className="cart-wrapper" onClick={this.showCartPage}>
          <i className="shopping-cart-icon"></i>
          <span className="amount">{total > 0 ? `(${total})` : ''}</span>
        </span>
      </span>
    )
  }
}

ShoppingCart.contextType = ShopifyContext;

export default ShoppingCart;

