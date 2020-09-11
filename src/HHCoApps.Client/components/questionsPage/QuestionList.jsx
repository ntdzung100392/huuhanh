import React from 'react';
import PropTypes from 'prop-types';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";
import { STRING_RESOURCES } from 'constants/questionPage/QuestionConstants';
import QuestionDetails from './QuestionDetails';
import Spinner from 'commonComponents/Spinner';

class QuestionList extends React.Component {
  constructor(props) {
    super(props);
    this.renderNoItemsToDisplay = this.renderNoItemsToDisplay.bind(this);
  }

  renderNoItemsToDisplay() {
    const noItemsToDisplay = this.props.noResultsFoundMessage ? this.props.noResultsFoundMessage : STRING_RESOURCES.noItemsToDisplay;
    return (
      <h4 style={{ textAlign: 'center' }}>
        {noItemsToDisplay}
      </h4>
    );
  }
  render() {
    if (!this.props.isLoaded) {
      return <Spinner />;
    }

    if (this.props.items.length > 0) {
      return this.props.items.map(item => <QuestionDetails key={item.id} item={item} />);
    }

    return (
      <React.Fragment>
        {this.renderNoItemsToDisplay()}
      </React.Fragment>
    );
  }
}

QuestionList.propTypes = {
  items: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  noResultsFoundMessage: PropTypes.string
};

export default QuestionList;