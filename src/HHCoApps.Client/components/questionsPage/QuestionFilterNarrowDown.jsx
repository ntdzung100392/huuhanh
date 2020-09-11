import React from 'react';
import PropTypes from 'prop-types';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";

class QuestionFilterNarrowDown extends React.Component {
  constructor(props) {
    super(props);

    this.renderElementsByRow = this.renderElementsByRow.bind(this);
  }

  renderElementsByRow(items) {
    return items.map(item =>
      <div key={item.paths.reduce((x, path) => `${x}${path}.`, '')} className="input-field" onClick={this.props.onClickIssueType}>
        <input type="radio" checked={this.props.selectedIssueType === item.name} id={"radio-options-" + item.name} className="input-field__radio" name="ask-expert-options" />
        <label className="input-field__radio-label">{item.name}</label>
      </div>
    );
  }

  render() {
    if (this.props.items.length > 0) {
      const rowElements = this.renderElementsByRow(this.props.items);

      return (
        <div className="row ask-expert-list-search__filters">
          <div className="col-sm-12 columns ask-expert-list-search__filters-input">
            <p>Narrow down further?</p>
            <div className="input-field-group input-field-group--compact input-field-group--inline">
              {rowElements}
            </div>
          </div>
        </div>
      );
    }

    return (null);
  }
}

QuestionFilterNarrowDown.propTypes = {
  items: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  selectedIssueType: PropTypes.string,
  onClickIssueType: PropTypes.func,
};

export default QuestionFilterNarrowDown;