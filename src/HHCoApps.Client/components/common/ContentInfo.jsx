import React from 'react';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";
import { STRING_RESOURCES } from "constants/AppConstants";
import PropTypes from 'prop-types';
import imageHelper from 'helpers/imageHelper';

class ContentInfo extends React.Component {
  constructor(props) {
    super(props);
    this.renderProducts = this.renderProducts.bind(this);
    this.renderArticles = this.renderArticles.bind(this);
  }

  getCropImageUrl(imageUrl, imageSize) {
    if (!imageSize)
      imageSize = AppConfig.imageSizes.threeColumnLeftAlign;
    return imageHelper.getCropImageUrl(imageUrl, imageSize.width, imageSize.height, imageSize.mode);
  }

  renderArticles(item) {
    let articlesClassName = this.props.isCarouselViewMode ? "col-md-12" : "col-md-4 col-sm-4 col-xs-12 ";

    return (
      <div className={articlesClassName}>
        <div className="blog-item">
          <a href={item.url}>
            <img src={item.imageUrl} alt={item.imageAlt} />
            <h4 className="blog-item__title">{item.title}</h4>
            <p className="blog-item__desc">
              {item.caption}
            </p>
            <div className="blog-item__btn">
              <span>{STRING_RESOURCES.viewPost}</span>
              <span className="icon-arrow-right-circle"></span>
            </div>
          </a>
        </div>
      </div>
    );
  }

  renderProducts(item) {
    let productsClassName = this.props.isCarouselViewMode ? "product-block text-center" : "col-xs-6 col-sm-4 col-md-4 col-lg-4 text-center search-bar__listing";

    return (
      <div className={productsClassName}>
        <a className="product-block__nav" href={item.url}>
          <img src={item.imageUrl} alt={item.imageAlt} className="product-block__image" />
          <h4 className="text-capitalize product-block__name responsive-title">{item.title}</h4>
          <p className="product-block__intro responsive-caption" style={{height: this.props.captionHeight !== 0 ? this.props.captionHeight : ''}}>
            {item.caption}
          </p>
          <i className="icon-arrow-right-circle"></i>
        </a>
      </div>
    );
  }

  render() {
    switch (this.props.itemView) {
      case STRING_RESOURCES.products:
        return this.renderProducts(this.props.item);
      case STRING_RESOURCES.articles:
      default:
        return this.renderArticles(this.props.item);
    }
  }
}

ContentInfo.propTypes = {
  item: SEARCH_ITEM_TYPE,
  itemView: PropTypes.string,
  captionHeight: PropTypes.number,
  isCarouselViewMode: PropTypes.bool
};

export default ContentInfo;