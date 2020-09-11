import React from "react";
import PropTypes from "prop-types";
import { PRODUCT_COLOR } from "constants/EntityTypes";

function ProductColorSelector(props) {
  const { colors, activeColor, setActiveColor, productTitle, colorCaption } = props;

  const renderColors = () => {
    let activeColorIndex = -1;
    activeColorIndex = colors.findIndex(x => x.colorUid === activeColor.colorUid);
    return colors.map((color, index) => (
      <div
        onClick={() => setActiveColor(color)}
        className={
          index == activeColorIndex && activeColorIndex > -1
            ? "product-detail__colors-block text-center active"
            : "product-detail__colors-block text-center"
        }
        key={index}
      >
        <div className="img-box">
          <img src={color.colorImageUrl} alt={color.colorName} />
        </div>
        <span>{color.colorName}</span>
      </div>
    ));
  };

  return (
    <div className="col-md-4 col-xs-12 col-sm-12 col-lg-4 px-0">
      <div className="steps">
        <span className="text-uppercase">Step 1</span>
        <h4 className="text-uppercase">{colorCaption ? colorCaption : 'Select your ' + productTitle + ' colour'}</h4>
      </div>
      <div className="product-detail__colors">
        <div className="is-flex flex-wrap">{renderColors()}</div>
      </div>
    </div>
  );
}

ProductColorSelector.propTypes = {
  colors: PropTypes.arrayOf(PRODUCT_COLOR),
  activeColor: PRODUCT_COLOR,
  setActiveColor: PropTypes.func,
  productTitle: PropTypes.string,
  colorCaption: PropTypes.string
};

export default ProductColorSelector;