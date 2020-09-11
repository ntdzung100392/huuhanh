import React from 'react';
import PropTypes from 'prop-types';

export default class StoreLocatorMap extends React.Component {
  constructor(props) {
    super(props);

    this.onKeyPress = this.onKeyPress.bind(this);
  }

  onKeyPress(e) {
    if (e && e.key === 'Enter') {
      this.props.onSubmitValue();
    }
  }

  render() {
    return (
      <React.Fragment>
        <div className="form-group">
          <label>ENTER POSTCODE</label>
          <div className="find-store__submit">
            <input onKeyPress={this.onKeyPress} type="text" id="search-box" placeholder="" className="search-bar__input" required autoComplete="off" />
            <a onClick={() => this.props.onSubmitValue()} className="is-flex align-items-center find-store__btn">
              SUBMIT
              <i className="icon-arrow-right-circle" style={{ fontSize: '35px', marginLeft: '5px' }}></i>
            </a>
          </div>
        </div>
      </React.Fragment>
    );
  }
}

StoreLocatorMap.propTypes = {
  onSubmitValue: PropTypes.func
};