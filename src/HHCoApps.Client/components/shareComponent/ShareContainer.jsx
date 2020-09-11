import React, { Component } from 'react';

class ShareContainer extends Component {
  constructor(props) {
    super(props);
    this.print = this.print.bind(this);
  }

  print() {
    window.print();
  }

  render() {
    const url = window.location.href;
    const sharingTwMessage = "Click on the link below to check out this Selleys product"
    const subject = "Hey thought you may like this";
    const sharingEmailMessage = `Click on the link below to check out this Selleys product ${url}`;

    const sharingFb = `http://www.facebook.com/sharer.php?u=${url}`;
    const sharingTw = `https://twitter.com/intent/tweet?text=${sharingTwMessage}&url=${url}`;
    const mailTo = `mailto:?subject=${subject}&body=${sharingEmailMessage}`;

    return (
      <div className="product-selector__products">
        <div className="sub-navigation__actions">
          <a href="#" className="sub-navigation__actions-link" data-modal-trigger="" data-modal-target="social-share" data-modal-type="social" data-content-only="true" data-social-type="article">
            <i className="icon icon-social-share"></i>
          </a>
          <a href="#" className="sub-navigation__actions-link" onClick={() => this.print()}>
            <i className="icon icon-ui-print"></i>
          </a>
        </div>
        <div className="social-share-wrapper hidden">
          <div className="social-share" id="social-share">
            <p className="social-share__share h7"><i className="icon icon-social-share"></i>Share</p>
            <p className="social-share__page-title h1">Share this product with your friends</p>
            <ul className="social-share__social-icons unstyled">
              <li>
                <a data-social="facebook" target="_blank" href={sharingFb} className="social-share__social-icons__icon icon__round">
                  <i className="icon icon-social-facebook" target="_blank"></i>
                </a>
              </li>
              <li>
                <a data-social="twitter" target="_blank" href={sharingTw} className="social-share__social-icons__icon icon__round">
                  <i className="icon icon-social-twitter" target="_blank"></i>
                </a>
              </li>
              <li>
                <a data-social="email" href={mailTo} className="social-share__social-icons__icon icon__round">
                  <i className="icon icon-envelope" target="_blank"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    )
  }
}

export default ShareContainer;