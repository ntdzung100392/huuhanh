import React from 'react';
import PropTypes from 'prop-types';
import { Tabs, Tab } from 'react-bootstrap';
import qs from 'query-string';

import { STRING_RESOURCES } from 'constants/AppConstants';
import ContentApi from 'apis/ContentApi';
import ContentInfoList from 'commonComponents/ContentInfoList';
import CenterActionLink from 'commonComponents/CenterActionLink';
import ShowMoreButton from 'commonComponents/ShowMoreButton';
import Spinner from 'commonComponents/Spinner';

class SearchPageContainer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoaded: false,
      activeTab: 'TopResults',
      captionHeight: 0,
      productTab: {
        items: [],
        totalItemCount: 0
      },
      articleTab: {
        items: [],
        totalItemCount: 0
      },
      otherTab: {
        items: [],
        totalItemCount: 0
      }
    };

    this.getTabState = this.getTabState.bind(this);
    this.onSelectTab = this.onSelectTab.bind(this);
    this.renderTabContent = this.renderTabContent.bind(this);
    this.renderNoItemsDisplayForAll = this.renderNoItemsDisplayForAll.bind(this);
    this.onClickShowMore = this.onClickShowMore.bind(this);
    this.reAdjustCaptionHeight = this.reAdjustCaptionHeight.bind(this);
  }

  componentDidMount() {
    var params = qs.parse(window.location.search, { ignoreQueryPrefix: true });
    const { productTab, articleTab, otherTab } = this.state;

    ContentApi.getProducts({
      paths: [this.props.productsPath],
      searchText: decodeURI(params.search)
    }).then(response => {
      this.setState({
        ...this.state,
        isLoaded: true,
        productTab: this.getTabState(productTab, response.data)
      });
      this.reAdjustCaptionHeight();
    });

    ContentApi.getArticles({
      paths: [this.props.blogsPath],
      searchText: decodeURI(params.search)
    }).then(response => {
      this.setState({
        ...this.state,
        isLoaded: true,
        articleTab: this.getTabState(articleTab, response.data)
      });
    });

    ContentApi.getOthers({
      paths: this.props.otherNodePaths,
      searchText: decodeURI(params.search)
    }).then(response => {
      this.setState({
        ...this.state,
        isLoaded: true,
        otherTab: this.getTabState(otherTab, response.data)
      });
    });
  }

  getTabState(currentTabState, responseData, appendItems) {
    let newState = {
      ...currentTabState,
      ...responseData
    };

    if (appendItems) {
      newState.items = [...currentTabState.items, ...responseData.items];
    }

    return newState;
  }

  onClickShowMore(tabKey, tabState) {
    if (tabState.isLastPage)
      return;

    const nextPage = tabState.pageNumber + 1;
    const params = qs.parse(window.location.search, { ignoreQueryPrefix: true });

    switch (tabKey) {
      case SEARCH_TAB_KEYS.products:
        {
          ContentApi.getProducts({
            paths: [this.props.productsPath],
            searchText: decodeURI(params.search)
          }, nextPage).then(response => {
            this.setState({
              ...this.state,
              productTab: this.getTabState(tabState, response.data, true)
            });
            this.reAdjustCaptionHeight();
          });
        }

        break;
      case SEARCH_TAB_KEYS.articles:
        {
          ContentApi.getArticles({
            paths: [this.props.blogsPath],
            searchText: decodeURI(params.search)
          }, nextPage).then(response => {
            this.setState({
              ...this.state,
              articleTab: this.getTabState(tabState, response.data, true)
            });
          });
        }

        break;
      case SEARCH_TAB_KEYS.others:
        {
          ContentApi.getOthers({
            paths: this.props.otherNodePaths,
            searchText: decodeURI(params.search)
          }, nextPage).then(response => {
            this.setState({
              ...this.state,
              otherTab: this.getTabState(tabState, response.data, true)
            });
          });
        }

        break;
    }
  }

  onSelectTab(tabKey) {
    this.setState({
      ...this.state,
      activeTab: tabKey
    });
  }

  renderTabContent(tabState, tabKey, tabTitle, displayViewAll, displayShowMore, isCarouselViewMode) {
    let renderItems = tabState.items;

    if (renderItems.length < 3) {
      isCarouselViewMode = false;
    }

    return (
      <React.Fragment>
        <div className="search-nav-title">
          <h3 className="h3">{tabTitle}</h3>
          {displayViewAll && isCarouselViewMode &&
            <CenterActionLink key={'link-' + tabKey} title={STRING_RESOURCES.viewAll} onClick={() => this.onSelectTab(tabKey)} />
          }
        </div>
        <ContentInfoList key={'list-' + tabKey} items={renderItems} itemView={tabTitle} isCarouselViewMode={isCarouselViewMode} captionHeight={this.state.captionHeight} />
        {!tabState.isLastPage && displayShowMore &&
          <ShowMoreButton key={'button-' + tabKey} title={STRING_RESOURCES.showMore} onClick={() => this.onClickShowMore(tabKey, tabState)} />
        }
      </React.Fragment>
    );
  }

  renderNoItemsDisplayForAll() {
    const params = qs.parse(window.location.search, { ignoreQueryPrefix: true });

    return (
      <React.Fragment>
        <h1 className="search-title">
          {STRING_RESOURCES.noResultsFoundForSearchTerm} <span style={{ fontWeight: 'bold' }}>{params.search}</span>
        </h1>
      </React.Fragment>
    );
  }

  reAdjustCaptionHeight() {
    const maxHeight = Math.max.apply(null, $(".react-app-root #SearchTab p.responsive-caption").map(function () {
      return $(this).height();
    }).get());

    if (maxHeight !== 0) {
      this.setState({
        ...this.state,
        captionHeight: maxHeight
      })
    }
  }

  render() {
    const { productTab, articleTab, otherTab } = this.state;
    const isDisplayNoResultForAll = this.state.isLoaded &&
      productTab.items.length === 0 & articleTab.items.length === 0 && otherTab.items.length === 0;

    if (!this.state.isLoaded) {
      return (<Spinner />);
    }

    if (isDisplayNoResultForAll) {
      return this.renderNoItemsDisplayForAll();
    }

    const params = qs.parse(window.location.search, { ignoreQueryPrefix: true });
    return (
      <React.Fragment>
        <h1 className="search-title">
          {STRING_RESOURCES.searchResult} <span style={{ fontWeight: 'bold' }}>{params.search}</span></h1>
        <Tabs id="SearchTab" activeKey={this.state.activeTab} onSelect={this.onSelectTab} >
          <Tab key={STRING_RESOURCES.topResults} eventKey={SEARCH_TAB_KEYS.topResults} title={STRING_RESOURCES.topResults}>
            {this.renderTabContent(productTab, SEARCH_TAB_KEYS.products, STRING_RESOURCES.products, true, false, true)}
            {this.renderTabContent(articleTab, SEARCH_TAB_KEYS.articles, STRING_RESOURCES.articles, true, false, true)}
            {this.renderTabContent(otherTab, SEARCH_TAB_KEYS.others, STRING_RESOURCES.others, true, false, true)}
          </Tab>
          <Tab key={STRING_RESOURCES.products} eventKey={SEARCH_TAB_KEYS.products} title={STRING_RESOURCES.products}>
            {this.renderTabContent(productTab, SEARCH_TAB_KEYS.products, STRING_RESOURCES.products, false, true, false)}
          </Tab>
          <Tab key={STRING_RESOURCES.articles} eventKey={SEARCH_TAB_KEYS.articles} title={STRING_RESOURCES.articles}>
            {this.renderTabContent(articleTab, SEARCH_TAB_KEYS.articles, STRING_RESOURCES.articles, false, true, false)}
          </Tab>
          <Tab key={STRING_RESOURCES.others} eventKey={SEARCH_TAB_KEYS.others} title={STRING_RESOURCES.others}>
            {this.renderTabContent(otherTab, SEARCH_TAB_KEYS.others, STRING_RESOURCES.others, false, true, false)}
          </Tab>
        </Tabs>
      </React.Fragment>
    );
  }
}

SearchPageContainer.propTypes = {
  productsPath: PropTypes.string,
  blogsPath: PropTypes.string,
  otherNodePaths: PropTypes.array
};

const SEARCH_TAB_KEYS = {
  topResults: "TopResults",
  products: "Products",
  articles: "Articles",
  others: "Others"
};

export default SearchPageContainer;