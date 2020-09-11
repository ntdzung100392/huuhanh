import React from 'react';
import { PRODUCT_DETAIL } from 'constants/EntityTypes';
import imageHelper from 'helpers/imageHelper';

function ProductNavigationDetailStep(props) {
  const { activeProduct, activeRootMenu } = props;
  return (
    <React.Fragment>
      {activeProduct.title &&
        (<div className="filter-menu__body filter-menu__body--third" id={activeProduct.id} style={activeRootMenu.hasChildItems ? { left: '66%' } : { left: '33%', paddingRight: '36%' }}>
          <a className="visible-xs hidden-md prev-step" data-toggle="collapse" data-target={"#" + activeProduct.id}>
            <i className="icon-left-arrow" />
          </a>
          <div className="filter-menu__column">
            <div className="filter-menu__block">
              <div className="is-flex justify-content-between align-items-center">
                <h3 className="heading-level-3 text-capitalize dashed product-name">
                  <a href={activeProduct.url} target="_blank">{activeProduct.title}</a>
                </h3>
                <div>
                  <a href={activeProduct.url} target="_blank">
                    <i className="icon-arrow-right-circle text-yellow" />
                  </a>
                </div>
              </div>
              <hr />
              <div className="description">
                <p className="text-white">
                  {activeProduct.caption}
                </p>
              </div>
              <div className="product-info">
                <div className="text-left view-all-products">
                  <a href={activeProduct.url} target="_blank" className="is-flex align-items-center">
                    <span className="text-uppercase">View product</span>
                    <i className="icon-arrow-right-circle text-yellow" />
                  </a>
                </div>
                <a href={activeProduct.url} target="_blank">
                  <img src={imageHelper.getCropImageUrl(activeProduct.imageUrl, 285, 401, "boxpad")} alt={activeProduct.imageAlt} className="img-responsive product-img" />
                </a>
              </div>
            </div>
          </div>
        </div>)
      }
    </React.Fragment>
  )
}

ProductNavigationDetailStep.propTypes = {
  activeProduct: PRODUCT_DETAIL
}

export default ProductNavigationDetailStep