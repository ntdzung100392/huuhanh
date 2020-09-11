import React from 'react';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";
import { CONTENT_LISTING_LAYOUTS } from "constants/responsiveItemListing/ResponsiveReactConstant"
import PropTypes from 'prop-types';
import imageHelper from 'helpers/imageHelper';

class ContentColumnItem extends React.Component {
  constructor(props) {
    super(props);

    this.renderTwoColumns = this.renderTwoColumns.bind(this);
    this.renderThreeColumnsWithFlexibleHeightMoreThanThreeItems = this.renderThreeColumnsWithFlexibleHeightMoreThanThreeItems.bind(this);
    this.renderThreeColumnsWithFlexibleHeightItems = this.renderThreeColumnsWithFlexibleHeightItems.bind(this);
    this.renderFourColumns = this.renderFourColumns.bind(this);
  }

  getCropImageUrl(imageUrl, imageSizes) {
    if (!imageSizes)
      imageSizes = AppConfig.imageSizes.thumbnail;

    return imageHelper.getCropImageUrl(imageUrl, imageSizes.width, imageSizes.height, imageSizes.mode);
  }

  renderTwoColumns(item) {
    return (
      <div className="col-md-6 col-sm-6 col-xs-12">
        <div className="promoWrapper ">
          <div className="promo-tile">
            <figure className="promo-tile--detail">
              <a href={item.url}><img className="promo-tile__hero" src={item.imageUrl} alt={item.imageAlt} /> </a>
              <figcaption className="promo-tile__text--listing">
                <button className="promo-tile__tag"><a href={item.parentUrl}>{item.parentTitle}</a></button>
                <button className="promo-tile__title"><a href={item.url}>{item.title}</a></button>
              </figcaption>
            </figure>
          </div>
          <p className="promo-tile__desc responsive-caption" style={{height: this.props.captionHeight !== 0 ? this.props.captionHeight : ''}}>{item.caption}</p>
          <div className="blog-item__btn">
            <a href={item.url} className="is-flex align-items-center justify-content-end">{this.props.viewDetailsLabel} <i className="icon-arrow-right-circle icon-arrow-on-top"></i></a>
          </div>
        </div>
      </div>
    );
  }

  renderThreeColumnsWithFlexibleHeightMoreThanThreeItems(item) {
    return (
      <div className="blog-item-wrapper">
        <div className="blog-item">
          <a href={item.url}>
            <img className="blog-item__img" src={item.imageUrl} alt={item.imageAlt} />
            <h4 className="blog-item__title">{item.title}</h4>
            <p className="blog-item__desc">{item.caption}</p>
            <div className="blog-item__btn">
              <span className="is-flex align-items-center justify-content-end">{this.props.viewDetailsLabel}</span>
              <i className="icon-arrow-right-circle icon-arrow-on-top"></i>
            </div>
          </a>
        </div>
      </div>
    );
  }

  renderThreeColumnsWithFlexibleHeightItems(item) {
    return (
      <div className="blog-item col-md-4 col-sm-6 col-xs-6">
        <a href={item.url}>
          <img className="blog-item__img" src={item.imageUrl} alt={item.imageAlt} />
          <h4 className="blog-item__title">{item.title}</h4>
          <p className="blog-item__desc">{item.caption}</p>
          <div className="blog-item__btn">
            <span>{this.props.viewDetailsLabel}</span>
            <i className="icon-arrow-right-circle icon-arrow-on-top"></i>
          </div>
        </a>
      </div>
    );
  }

  renderThreeColumnsWithAttributes(item) {
    const attributes = item.relatedProducts.map((relatedProduct, index) => this.renderProductAttributes(relatedProduct, index));

    return (
      <div className="col-md-4 col-sm-6 col-xs-12">
        <div className="promoWrapper">
          <a className="promo-tile" href={item.url} >
            <img className="promo-tile__hero" src={item.imageUrl} alt={item.imageAlt} />
            <h3>{item.title}</h3>
            <p>{item.caption}</p>
          </a>
          <div className="get-the-look-category__bottom">
            <div className="get-the-look-category__bottom--left">
              {attributes}
            </div>
          </div>
        </div>
      </div>
    );
  }

  renderProductAttributes(relatedProduct, index) {
    if (index < 2) {
      let color = '';
      let relatedProductUri = '';

      if (relatedProduct.color) {
        color = '?selectedColor=' + encodeURI(relatedProduct.color.toLowerCase());
      }

      if (relatedProduct.productUrl) {
        relatedProductUri = relatedProduct.productUrl + color;
      }

      return (
        <div className="get-the-look-category__btn">
          <a key={relatedProduct.title} className="btn" href={relatedProductUri}>
            <span className="icon-relate-product">
              <i className={(relatedProduct.productSubCategory) ? 'icon icon-' + relatedProduct.productSubCategory.toLowerCase() : ''}></i>
            </span>{relatedProduct.title}
          </a>
        </div>
      );
    }
  }

  renderFourColumns(item) {
    return (
      <div className="col-xs-6 col-sm-6 col-md-3 col-lg-3">
        <div className="product-block text-center">
          <a href={item.url} className="product-block__nav">
            <img className="product-block__image" src={item.imageUrl} alt={item.imageAlt} />
            <h4 className="text-capitalize product-block__name responsive-title">{item.title}</h4>
            <p className="product-block__intro responsive-caption" style={{height: this.props.captionHeight !== 0 ? this.props.captionHeight : ''}}>{item.caption}</p>
            <i className="icon-arrow-right-circle icon-arrow-on-top"></i>
          </a>
        </div>
      </div>
    );
  }

  render() {
    const item = this.props.item;
    switch (this.props.itemView) {
      case CONTENT_LISTING_LAYOUTS.twoColumn:
        return this.renderTwoColumns(item);
      case CONTENT_LISTING_LAYOUTS.threeColumnsWithFlexibleHeightMoreThanThreeItems:
        return this.renderThreeColumnsWithFlexibleHeightMoreThanThreeItems(item);
      case CONTENT_LISTING_LAYOUTS.threeColumnsWithFlexibleHeightItems:
        return this.renderThreeColumnsWithFlexibleHeightMoreThanThreeItems(item);
      case CONTENT_LISTING_LAYOUTS.threeColumnsWithAttributes:
        return this.renderThreeColumnsWithAttributes(item);
      case CONTENT_LISTING_LAYOUTS.fourColumns:
        return this.renderFourColumns(item);
      default:
        return this.renderTwoColumns(item);
    }
  }
}

ContentColumnItem.propTypes = {
  item: SEARCH_ITEM_TYPE,
  itemView: PropTypes.string,
  captionHeight: PropTypes.number,
  viewDetailsLabel: PropTypes.string
};

export default ContentColumnItem;