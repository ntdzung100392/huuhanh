import React from 'react';

class ShowMoreButton extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    let buttonClassName = "btn is-flex align-items-center justify-content-center";

    if (this.props.isClickShowMore) {
      buttonClassName += " is-loading";
    }

    return (
      <div className="product-list__load-more text-center">
        <button className={buttonClassName} id="load-more" onClick={this.props.onClick} disabled={this.props.isClickShowMore ? true : false}>
          <img src="/assets/images/icons/icon-load-more.png" alt="load more" />
          <span className="text-uppercase">{this.props.title}</span>
        </button>
        <img src="/assets/images/icons/ajax-loader.gif" className="product-list__load-more--spinning" alt="ajax-load" />
      </div>
    );
  }
}

export default ShowMoreButton;