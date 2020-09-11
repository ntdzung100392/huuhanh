import React from 'react';
import PropTypes from 'prop-types';
import QuestionFilterNarrowDown from './QuestionFilterNarrowDown';
import { STRING_RESOURCES } from 'constants/questionPage/QuestionConstants';
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";

class QuestionFilterBlock extends React.Component {
  constructor(props) {
    super(props);
    this.onTopicChange = this.onTopicChange.bind(this);
  }

  onTopicChange(e) {
    this.props.onTopicChange(e.target.value);
  }

  render() {
    const rowElements = this.props.topics.map(topic => <option key={topic.path} value={topic.path}>{topic.name}</option>);
    return (
      <div className="ask-expert-list-search">
        <div className="row">
          <div className="col-md-6 col-sm-6 col-xs-12">
            <label>{STRING_RESOURCES.searchAnswerTitle}
              <input type="text" className="input-field__text" value={this.props.searchText} placeholder="What's your question?" name="searchField" onChange={this.props.onSearchTextChange} />
            </label>
          </div>
          <div className="ask-expert-list-search__and col-md-1 col-sm-1 col-xs-12">AND</div>
          <div className="col-md-5 col-sm-5 col-xs-12">
            <label>Sort By
              <select className="input-field__select" onChange={this.onTopicChange}>
                <option key="" value="">All Topics</option>
                {rowElements}
              </select>
            </label>
          </div>
        </div>
        <div className="row">
          <div className="col-xs-12 ask-expert-list-search__buttons">
            <button type="button" className="button unstyled large" onClick={this.props.resetFilter}>
              <span className="button__text">{STRING_RESOURCES.resetFields}</span>
            </button>
            <button type="button" className="button large" disabled={this.props.isDisabledSearch} onClick={this.props.onClickFindAnswers}>
              <span className="button__text" onClick={this.props.onClick}>{STRING_RESOURCES.findAnswers}</span>
            </button>
          </div>
        </div>
        {(this.props.issueTypes.length > 0 && this.props.isDisplayNarrowDown) &&
          <QuestionFilterNarrowDown items={this.props.issueTypes} onClickIssueType={this.props.onClickIssueType} selectedIssueType={this.props.selectedIssueType} />
        }
      </div>
    );
  }
}

QuestionFilterBlock.propTypes = {
  topics: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  issueTypes: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  isDisplayNarrowDown: PropTypes.bool,
  isDisabledSearch: PropTypes.bool,
  onClickFindAnswers: PropTypes.func,
  onClickIssueType: PropTypes.func,
  resetFilter: PropTypes.func,
  onTopicChange: PropTypes.func,
};

export default QuestionFilterBlock;