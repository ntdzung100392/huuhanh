import React from "react";
import PropTypes from "prop-types";
import { PRODUCT_TIMBER } from "constants/EntityTypes";

function ProductTimberSelector(props) {
  const { activeTimbers, setActiveTimber, selectedTimber, timberCaption } = props;

  const renderTimbers = () => {
    if (Object.keys(activeTimbers).length > 0) {
      return activeTimbers.map((timber, index) => (
        <li key={index} className={timber.timberUid === selectedTimber.timberUid ? "active" : ""}
          onClick={() => setActiveTimber(timber)}
        >
          <a
            data-toggle="tab"
            className="text-uppercase"
            href={`#${timber.timberUid}`}
          >
            {timber.timberName}
          </a>
        </li>
      ));
    }
  };

  const renderCoats = () => {
    const activeClass = "tab-pane fade in active";
    if (Object.keys(activeTimbers).length > 0) {
      return activeTimbers.map((timber, index) => (
        <div
          id={`${timber.timberUid}`}
          className={
            timber.timberUid === selectedTimber.timberUid ? activeClass : "tab-pane fade"
          }
          key={index}
        >
          <div className="is-flex flex-wrap">
            {timber.coats.map((coat, coatIndex) => (
              <div className="texture text-center" key={coatIndex}>
                <img src={coat.coatImageUrl} alt={coat.coatOption} />
                <p className="text-capitalize">{coat.coatOption}</p>
              </div>
            ))}
          </div>
        </div>
      ));
    }
  };

  const updateAccuracyColorButtonVisibility = () => {
    const accuracyColorItem = document.getElementById('accuracy-color');
    if (accuracyColorItem) {
      accuracyColorItem.style.display = (Object.keys(activeTimbers) || []).length > 0 ? "block" : "none";
    }
  };

  updateAccuracyColorButtonVisibility();

  return (
    <React.Fragment>
      {Object.keys(activeTimbers).length > 0 && (
        <div className="col-md-7 col-xs-12 col-sm-12 col-lg-8 px-0 product-detail--right">
          <div className="steps">
            <span className="text-uppercase">Step 2</span>
            <h4 className="text-uppercase">
              {timberCaption}
            </h4>
          </div>
          <div className="product-detail--variants">
            <div className="row">
              <div className="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <ul className="nav nav-tabs">{renderTimbers()}</ul>
                <div className="tab-content">{renderCoats()}</div>
              </div>
            </div>
          </div>
        </div>
      )}
    </React.Fragment>
  );
}

ProductTimberSelector.propTypes = {
  timberCaption: PropTypes.string,
  activeTimbers: PropTypes.arrayOf(PRODUCT_TIMBER),
  setActiveTimber: PropTypes.func,
  selectedTimber: PRODUCT_TIMBER,
};

export default ProductTimberSelector;
