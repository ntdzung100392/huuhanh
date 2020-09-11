import React from 'react';
import { MINI_WISHLIST_ITEM } from 'constants/EntityTypes';
import { MONEY_FORMAT } from 'constants/AppConstants';
import { VIEW_MODE } from 'constants/wishListPage/WishListConstant';
import PropTypes from 'prop-types';
import * as numeral from 'numeral';

class WishListContentRow extends React.Component {
  constructor(props) {
    super(props);

    this.renderMiniWishListRow = this.renderMiniWishListRow.bind(this);
    this.renderWishListDetailsRow = this.renderWishListDetailsRow.bind(this);
    this.renderLineItem = this.renderLineItem.bind(this);
  }

  renderWishListDetailsRow() {
    if (this.props.item) {
      const product = this.props.item;
      return (
        <tr>
          <td>
            <div className="cart-item tables">
              <div className="cart-item__image-wrapper">
                <img src={product.productImageUrl} alt={product.productImageAlt} />
              </div>
              <div className="cart-item__info">
                <h4 className="cart-item__title heading"><a href={product.productUri}>{product.title}</a></h4>
                <p>{product.caption}</p>
              </div>
            </div>
          </td>
          <td>
            <div className="cart-table__colour">
              <img src={product.colorImageUrl} />
              <span className="bold">{product.colorName}</span>
            </div>
          </td>
          <td className="bold">{product.size}</td>
          <td>
            <div className="cart-item__actions tables">
              <div className="cart-item__quantity-selector">
                <div className="quantity-selector">
                  <a onClick={() => this.props.onChangeQuantity(product, true)} className="quantity-selector__button"><span className="icon-minus"></span></a>
                  <input readOnly="readOnly" type="text" value={product.quantity} className="quantity-selector__current-quantity" />
                  <a onClick={() => this.props.onChangeQuantity(product, false)} className="quantity-selector__button"><span className="icon-plus"></span></a>
                </div>
              </div>
              <a onClick={() => this.props.onRemoveItem(product)} className="cart-item__remove">Remove</a>
            </div>
          </td>
        </tr>
      );
    }

    return <tr></tr>;
  }

  renderLineItem(product) {
    let template = null;

    if (this.props.enableShopping) {
      template = <React.Fragment>
        <p>Colour: <img src={product.colorImageUrl} /> <span className="bold-color">{product.colorName}</span></p>
        <div className="size-price-container">
          <p>Size: <span className="bold">{product.size}</span></p>
          <p>Price: <span className="bold">{numeral(product.variant.price * product.quantity).format(MONEY_FORMAT)}</span></p>
        </div>
      </React.Fragment>
    } else {
      template = <React.Fragment>
        <p>Colour: <img src={product.colorImageUrl} /> <span className="bold-color">{product.colorName}</span></p>
        <p>Size: <span className="bold">{product.size}</span></p>
      </React.Fragment>
    }

    return template;
  }

  renderMiniWishListRow() {
    if (this.props.item) {
      const product = this.props.item;

      return (
        <React.Fragment>
          <div className="cart-item">
            <div className="cart-item__imageWrapper">
              <img src={product.productImageUrl} alt={product.productImageAlt} />
            </div>
            <div className="cart-item__info">
              <h4 className="cart-item__title heading"><a href={product.productUri}>{product.title}</a></h4>
              <div className="cart-item__meta">
                {this.renderLineItem(product)}
              </div>
              <div className="cart-item__actions">
                <div className="cart-item__quantity-selector">
                  <div className="quantity-selector">
                    <a onClick={() => this.props.onChangeQuantity(product, true)} className="quantity-selector__button"><span className="icon-minus"></span></a>
                    <input readOnly="readOnly" type="text" value={product.quantity} className="quantity-selector__current-quantity" />
                    <a onClick={() => this.props.onChangeQuantity(product, false)} className="quantity-selector__button"><span className="icon-plus"></span></a>
                  </div>
                </div>
                <a onClick={() => this.props.onRemoveItem(product)} className="cart-item__remove">Remove</a>
              </div>
              {
                this.props.enableShopping && !this.props.item.sku &&
                <div className="invalid-variant">
                  <p>This variant is not valid on store</p>
                </div>
              }
            </div>
          </div>
        </React.Fragment>
      );
    }

    return (
      <div className="cart-item">
      </div>
    );
  }

  render() {
    switch (this.props.viewMode) {
      case VIEW_MODE.miniWishList:
        return this.renderMiniWishListRow();
      case VIEW_MODE.wishListDetails:
        return this.renderWishListDetailsRow();
      default:
        return this.renderMiniWishListRow();
    }
  }
}

WishListContentRow.propTypes = {
  item: MINI_WISHLIST_ITEM,
  viewMode: PropTypes.string,
  onChangeQuantity: PropTypes.func,
  onRemoveItem: PropTypes.func,
  enableShopping: PropTypes.bool
};

export default WishListContentRow;