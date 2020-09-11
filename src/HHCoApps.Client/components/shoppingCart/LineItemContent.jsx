import React from 'react';
import { SHOPPING_LINE_ITEM } from 'constants/EntityTypes';
import PropTypes from 'prop-types';

class LineItemContent extends React.Component {
  constructor(props) {
    super(props);

    this.renderLineItem = this.renderLineItem.bind(this);
    this.renderSelectedOption = this.renderSelectedOption.bind(this);
  }

  renderSelectedOption(selectedOptions) {
    return (selectedOptions || []).map(item => {
      return <p key={item.name + '/' + item.value} className={item.name}>{item.name}: {item.value}</p>
    });
  };


  renderLineItem() {
    if (this.props.item) {
      const lineItem = this.props.item;
      return (
        <div className="cart-item">
          <div className="cart-item__imageWrapper">
            <img src={lineItem.variant.image.src} alt={lineItem.variant.image.altText} />
          </div>
          <div className="cart-item__info">
            <h4 className="cart-item__title heading"><a href={lineItem.productUri}>{lineItem.title}</a></h4>
            <div className="cart-item__meta">
              {this.renderSelectedOption(lineItem.variant.selectedOptions)}
            </div>
            <div className="cart-item__actions">
              <div className="cart-item__quantity-selector">
                <div className="quantity-selector">
                  <a onClick={() => this.props.onChangeQuantity(lineItem, true)} className="quantity-selector__button"><span className="icon-minus"></span></a>
                  <input readOnly="readOnly" type="text" value={lineItem.quantity} className="quantity-selector__current-quantity" />
                  <a onClick={() => this.props.onChangeQuantity(lineItem, false)} className="quantity-selector__button"><span className="icon-plus"></span></a>
                </div>
              </div>
              <a onClick={() => this.props.onRemoveItem(lineItem)} className="cart-item__remove">Remove</a>
            </div>
          </div>
        </div>
      );
    }

    return (
      <div className="cart-item">
      </div>
    );
  }

  render() {
    return this.renderLineItem();
  }
}

LineItemContent.propTypes = {
  item: SHOPPING_LINE_ITEM,
  onChangeQuantity: PropTypes.func,
  onRemoveItem: PropTypes.func
};

export default LineItemContent;