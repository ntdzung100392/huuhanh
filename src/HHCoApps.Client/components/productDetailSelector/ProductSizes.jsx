import React from "react";
import PropTypes from "prop-types";

function ProductSizes(props) {
  const { sizes, selectedSize, setActiveSize } = props;

  const goToSlideBySize = (sizeValue) => {
    if (sizeValue) {
      const slides = $(".product-detail__slider .slick-slide:not(.slick-cloned)");
      if (slides.length > 0) {
        for (let i = 0; i < slides.length; i++) {
          let slide = $(slides[i]);
          let slideSize = slide.attr("data-size");
          if (slideSize == sizeValue) {
            let slideIndex = slide.attr("data-slick-index");
            $(".product-detail__slider").slick("goTo", slideIndex);
          }
        }
      }
    }
  }

  const renderSizes = () => {
    goToSlideBySize(selectedSize);
    return sizes.map((size, index) => (
      <option key={index} value={size}>
        {size}
      </option>
    ));
  };

  const onProductSizeChanged = (e) => {
    goToSlideBySize(e.target.value);
    setActiveSize(e.target.value);
  }

  return (
    <div className="product-detail__sizes is-flex col-md-4 px-0">
      <h4 className="text-uppercase">Available sizes</h4>
      <label htmlFor="available-sizes">
        <select name="available-sizes" id="available-sizes" className="select-css" onChange={onProductSizeChanged} value={selectedSize}>
          {renderSizes()}
        </select>
      </label>
    </div>
  );
}

ProductSizes.proptypes = {
  sizes: PropTypes.arrayOf(PropTypes.string),
  setActiveSize: PropTypes.func,
  selectedSize: PropTypes.string
}

export default ProductSizes;