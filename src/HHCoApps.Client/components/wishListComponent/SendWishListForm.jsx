import React from 'react';
import PropTypes from 'prop-types';
import { EMAIL_FORM_ERROR_FIELDS } from 'constants/EntityTypes';
import { STRING_RESOURCES } from 'constants/wishListPage/WishListConstant';

class SendWishListForm extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <React.Fragment>
        <div className="wish-list__send">
          <h3>Send WishList of <span className="list-number">{this.props.itemsQuantity}</span> Items</h3>
          <hr />
          <p>{STRING_RESOURCES.formInstruction}</p>
          <form className="form-submit">
            <div className="form-group">
              <label>{STRING_RESOURCES.formFirstName}</label>
              <input type="text" className="form-control" name="firstName" value={this.props.firstName} onChange={this.props.onChangeValue} />
              {this.props.fieldErrors.firstName && this.props.fieldErrors.firstName.length > 0 && <span className='error'>{this.props.fieldErrors.firstName}</span>}
            </div>
            <div className="form-group">
              <label>{STRING_RESOURCES.formLastName}</label>
              <input type="text" className="form-control" name="lastName" value={this.props.lastName} onChange={this.props.onChangeValue} />
              {this.props.fieldErrors.lastName && this.props.fieldErrors.lastName.length > 0 && <span className='error'>{this.props.fieldErrors.lastName}</span>}
            </div>
            <div className="form-group">
              <label>{STRING_RESOURCES.formEmail}</label>
              <input type="text" className="form-control" name="email" value={this.props.email} onChange={this.props.onChangeValue} />
              {this.props.fieldErrors.email && this.props.fieldErrors.email.length > 0 && <span className='error'>{this.props.fieldErrors.email}</span>}
            </div>
            <hr />
            <div className="form-group">
              {this.props.errorMessage && this.props.errorMessage.length > 0 && <span className='error'>{this.props.errorMessage}</span>}
            </div>
            <button disabled={this.props.itemsQuantity === 0 || this.props.isLoading}
              className="g-recaptcha" data-sitekey={AppConfig.googleRecaptchaKey.publicKey} data-size="invisible" type="button"
              onClick={this.props.onSubmitEmailRequest}>
              {this.props.isLoading ? <img src="/assets/images/icons/ajax-spinner.gif" className="wish-list__send-spinning" /> : ''}
              {STRING_RESOURCES.formSubmitButtonText}
            </button>
          </form>
        </div>
      </React.Fragment>
    )
  }
}

SendWishListForm.propTypes = {
  itemsQuantity: PropTypes.number,
  firstName: PropTypes.string,
  lastName: PropTypes.string,
  email: PropTypes.string,
  isLoading: PropTypes.bool,
  fieldErrors: EMAIL_FORM_ERROR_FIELDS,
  errorMessage: PropTypes.string,
  onChangeValue: PropTypes.func,
  onSubmitEmailRequest: PropTypes.func
};

export default SendWishListForm;