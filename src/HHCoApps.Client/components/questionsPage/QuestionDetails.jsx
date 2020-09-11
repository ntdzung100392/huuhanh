import React from 'react';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";

class QuestionDetails extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="ask-expert-list-results__row row">
        <div className="col-md-1 col-sm-2 col-xs-2 ask-expert-list-results__icon">
          <div className="ask-expert-list-results__icon__image"><i className="icon icon-user"></i></div>
          <div className="ask-expert-list-results__icon__name"></div>
        </div>
        <div className="col-md-10 col-sm-10 col-xs-10"><span className="ask-expert-list-results__description">
          <a href={this.props.item.url}>{this.props.item.title}</a></span>
        </div>
      </div>
    );
  }
}

QuestionDetails.propTypes = {
  item: SEARCH_ITEM_TYPE,
};

export default QuestionDetails;