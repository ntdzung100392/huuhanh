import React from 'react';
import PropTypes from 'prop-types';
import QuestionApi from 'apis/QuestionApi';
import ShowMoreButton from 'commonComponents/ShowMoreButton';
import QuestionList from './QuestionList';
import QuestionFilterBlock from './QuestionFilterBlock';

import { STRING_RESOURCES } from 'constants/questionPage/QuestionConstants';

class QuestionsPageContainer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      topics: [],
      isDisplayNarrowDown: false,
      issueTypes: [],
      items: [],
      path: '',
      paths: [],
      searchText: '',
      selectedIssueType: '',
      pageNumber: 1,
      totalItemCount: 0,
      isDisabledSearch: true,
      isLastPage: true,
      noResultsFoundMessage: '',
      isLoaded: false
    };

    this.onClickShowMore = this.onClickShowMore.bind(this);
    this.onClickFindAnswers = this.onClickFindAnswers.bind(this);
    this.resetFilter = this.resetFilter.bind(this);
    this.onClickIssueType = this.onClickIssueType.bind(this);
    this.onTopicChange = this.onTopicChange.bind(this);
    this.onSearchTextChange = this.onSearchTextChange.bind(this);
    this.isDisabledBtnSearch = this.isDisabledBtnSearch.bind(this);
    this.loadIssueTypes = this.loadIssueTypes.bind(this);
  }

  componentDidMount() {
    QuestionApi.getQuestions(this.props.categoryId)
      .then(response => {
        this.setState({
          ...this.state,
          ...response.data,
          noResultsFoundMessage: response.data.noResultsFoundMessage
        });
      });

    QuestionApi.searchQuestionsFilterUrl({ path: this.props.categoryId }, 1)
      .then(response => {
        this.appendItemsFromResponseToState(response);
        this.setState({ isLoaded: true });
      });
  }

  appendItemsFromResponseToState(response, keepIssueTypes) {
    var newState = { ...this.state, ...response.data, issueTypes: keepIssueTypes ? this.state.issueTypes : response.data.issueTypes };
    newState.items = [...this.state.items, ...response.data.items];

    this.setState(newState);
  }

  onClickShowMore() {
    if (this.state.isLastPage)
      return;
    const nextPage = this.state.pageNumber + 1;
    QuestionApi.searchQuestionsFilterUrl({ path: this.state.path, paths: this.state.paths, searchText: this.state.searchText }, nextPage)
      .then(response => {
        this.appendItemsFromResponseToState(response, true);
      });
  }

  onClickIssueType(e) {
    const issueTypePath = this.state.issueTypes.find(item => item.name === e.target.innerText);
    if (issueTypePath) {
      this.loadIssueTypes(issueTypePath.paths, e.target.innerText);
    }
  }

  loadIssueTypes(paths, nameIssue) {
    this.setState({
      ...this.state,
      paths: paths,
      selectedIssueType: nameIssue,
    });

    QuestionApi.searchQuestionsFilterUrl({ path: this.state.path, paths: paths, searchText: this.state.searchText }, 1)
      .then(response => {
        this.setState({
          ...this.state,
          ...response.data,
          issueTypes: this.state.issueTypes
        });
      });
  }

  onClickFindAnswers() {
    this.setState({
      ...this.state,
      selectedIssueType: '',
      paths: [],
    });

    QuestionApi.searchQuestionsFilterUrl({ path: this.state.path, searchText: this.state.searchText }, 1)
      .then(response => {
        this.setState({
          ...this.state,
          ...response.data,
          isDisplayNarrowDown: true
        });
      });
  }

  resetFilter() {
    this.setState({
      ...this.state,
      topics: [],
      searchText: '',
      paths: [],
      path: ''
    });

    QuestionApi.searchQuestionsFilterUrl({ path: this.props.categoryId }, 1)
      .then(response => {
        this.setState({
          ...this.state,
          ...response.data,
          isDisplayNarrowDown: false,
          isDisabledSearch: true
        });

        QuestionApi.getQuestions(this.props.categoryId)
          .then(topics => {
            this.setState({
              ...this.state,
              ...topics.data,
            });
          });
      });
  }

  onTopicChange(path) {
    this.setState({
      ...this.state,
      isDisplayNarrowDown: false,
      path: path,
      paths: [],
      isDisabledSearch: this.isDisabledBtnSearch(path, this.state.searchText)
    });
  }

  onSearchTextChange(e) {
    this.setState({
      ...this.state,
      isDisplayNarrowDown: false,
      searchText: e.target.value,
      isDisabledSearch: this.isDisabledBtnSearch(this.state.path, e.target.value)
    });
  }

  isDisabledBtnSearch(path, searchText) {
    return (path || searchText) ? false : true;
  }

  render() {
    let resultFound;
    if (this.state.isLoaded && this.state.items.length !== 0) {
      resultFound = (<h3>{this.state.totalItemCount} {STRING_RESOURCES.resultsFound}</h3>);
    }

    return (
      <React.Fragment>
        <QuestionFilterBlock
          topics={this.state.topics}
          issueTypes={this.state.issueTypes}
          isDisplayNarrowDown={this.state.isDisplayNarrowDown}
          isDisabledSearch={this.state.isDisabledSearch}
          onClickFindAnswers={this.onClickFindAnswers}
          onClickIssueType={this.onClickIssueType}
          resetFilter={this.resetFilter}
          onTopicChange={this.onTopicChange}
          onSearchTextChange={this.onSearchTextChange}
          searchText={this.state.searchText}
          selectedIssueType={this.state.selectedIssueType} />
        <div className="ask-expert-list-results">
          {resultFound}
          <QuestionList
            items={this.state.items}
            isLoaded={this.state.isLoaded}
            noResultsFoundMessage={this.state.noResultsFoundMessage} />
        </div>
        {!this.state.isLastPage && this.state.items.length > 0 &&
          <ShowMoreButton title={STRING_RESOURCES.showMore} onClick={this.onClickShowMore} />
        }
      </React.Fragment>
    );
  }
}

QuestionsPageContainer.propTypes = {
  categoryId: PropTypes.string
};

export default QuestionsPageContainer;