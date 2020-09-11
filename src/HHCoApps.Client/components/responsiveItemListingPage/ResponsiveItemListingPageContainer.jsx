import React from 'react';
import FilterToolbar from 'commonComponents/FilterToolbar';
import PropTypes from 'prop-types';
import ReactItemListingApi from 'apis/ReactItemListingApi';
import ContentApi from 'apis/ContentApi';
import ShowMoreButton from 'commonComponents/ShowMoreButton';
import ContentRowItems from 'commonComponents/ContentRowItems';
import Spinner from 'commonComponents/Spinner';

class ResponsiveItemListingPageContainer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      primaryFilterCategories: [],
      activePrimaryFilterItems: [],
      activePrimaryFilterCategory: "",
      filterCategories: [],
      activeFilterCategory: "",
      activeFilterItem: "",
      expandedFilterCategories: [],
      activeFilterItems: [],
      items: [],
      pageNumber: 1,
      isLastPage: true,
      showModal: false,
      isLoaded: false,
      isClickShowMore: false,
      captionHeight: 0
    };

    this.onClickShowMore = this.onClickShowMore.bind(this);
    this.onClickFilterItem = this.onClickFilterItem.bind(this);
    this.buildFilterCriteria = this.buildFilterCriteria.bind(this);
    this.clearActiveFilterItems = this.clearActiveFilterItems.bind(this);
    this.initialData = this.initialData.bind(this);
    this.getFilterCategoryModelStatics = this.getFilterCategoryModelStatics.bind(this);
    this.onClickOpenModal = this.onClickOpenModal.bind(this);
    this.onClickCloseModal = this.onClickCloseModal.bind(this);
    this.getDefaultFilterCategory = this.getDefaultFilterCategory.bind(this);
    this.getGroupByFilterValues = this.getGroupByFilterValues.bind(this);
    this.reAdjustCaptionHeight = this.reAdjustCaptionHeight.bind(this);
  }

  initialData(criteria) {
    ContentApi.searchContents({
      paths: [this.props.categoryId],
      includedContentIds: this.props.includedContentIds,
      criteria: criteria,
      defaultCriteria: this.props.defaultFilteringBy,
      sortingRequest: {
        sortBy: this.props.sortBy, orderType: this.props.orderType
      }
    }, 1, this.props.itemsPerPage, this.props.contentType, this.props.itemView)
      .then(result => {
        this.setState({ ...this.state, ...result.data, isLoaded: true });
      });
  }

  componentDidMount() {
    if (this.props.enabledFilter) {
      ReactItemListingApi.getPrimaryFilterCategoryModels(this.props.primaryFilterIds)
        .then(first => {
          if (first.data.length > 0 && first.data[0].items.length > 0) {
            this.setState({
              ...this.state,
              primaryFilterCategories: first.data,
              activePrimaryFilterCategory: first.data[0].items[0].key,
              activePrimaryFilterItems: [first.data[0].items[0]]
            });
          }

          ReactItemListingApi.getFilterCategoryModel(this.props.filterBy, this.state.activePrimaryFilterCategory, this.props.groupByFilter)
            .then(second => {
              this.setState({
                ...this.state,
                filterCategories: [...second.data],
              });

              const criteria = this.buildFilterCriteria(this.state.activeFilterItems, this.state.activePrimaryFilterItems);
              this.initialData(criteria);

              this.getFilterCategoryModelStatics(criteria);
            });
        });
    } else {
      ContentApi.searchContents({
        paths: [this.props.categoryId],
        includedContentIds: this.props.includedContentIds,
        defaultCriteria: this.props.defaultFilteringBy,
        sortingRequest: {
          sortBy: this.props.sortBy, orderType: this.props.orderType
        }
      }, 1, this.props.itemsPerPage, this.props.contentType, this.props.itemView)
        .then(result => {
          this.setState({ ...this.state, ...result.data, isLoaded: true });
          this.reAdjustCaptionHeight();
        });
    }
  }

  appendItemsFromResponseToState(response) {
    var newState = { ...this.state, ...response.data, isClickShowMore: false };
    newState.items = [...this.state.items, ...response.data.items];

    this.setState(newState);
  }

  onClickShowMore() {
    if (this.state.isLastPage)
      return;

    const criteria = this.buildFilterCriteria(this.state.activeFilterItems, this.state.activePrimaryFilterItems);
    const nextPage = this.state.pageNumber + 1;

    this.setState({
      ...this.state,
      isClickShowMore: true
    }, () => {
      ContentApi.searchContents({
        paths: [this.props.categoryId],
        includedContentIds: this.props.includedContentIds,
        defaultCriteria: this.props.defaultFilteringBy,
        sortingRequest: { sortBy: this.props.sortBy, orderType: this.props.orderType },
        criteria: criteria
      }, nextPage, this.props.itemsPerPage, this.props.contentType, this.props.itemView)
        .then(response => {
          this.appendItemsFromResponseToState(response);
          this.reAdjustCaptionHeight();
        });
    });
  }

  onClickFilterItem(filterItem) {
    const activePrimaryFilterItems = [...this.state.activePrimaryFilterItems];
    let activeFilterItems = [...this.state.activeFilterItems];
    if (filterItem) {
      activeFilterItems = [filterItem];
    }
    else {
      activeFilterItems = [];
    }

    this.setState({
      ...this.state,
      activeFilterItem: filterItem ? filterItem.key : '',
      activeFilterItems: activeFilterItems,
      isLoaded: false
    }, () => {
      const criteria = this.buildFilterCriteria(activeFilterItems, activePrimaryFilterItems);
      this.initialData(criteria);
      this.getFilterCategoryModelStatics(criteria);
      this.reAdjustCaptionHeight();
    });
  }

  buildFilterCriteria(activeFilterItems, activePrimaryFilterItems) {
    const filterCriteria = [];
    const activeFilters = [...activeFilterItems, ...activePrimaryFilterItems];
    for (var index = 0; index < activeFilters.length; index++) {
      const filterItem = activeFilters[index];
      const filterCriterion = {
        filterCategoryKey: filterItem.key.split(":")[0],
        filterValue: filterItem.name,
        isPrimaryFilter: filterItem.isPrimaryFilter ? filterItem.isPrimaryFilter : false,
        isPrimarySubFilter: filterItem.isPrimarySubFilter ? filterItem.isPrimarySubFilter : false,
        primaryFilterKey: filterItem.isPrimarySubFilter ? filterItem.primaryFilterKey : ''
      };

      filterCriteria.push(filterCriterion);
    }

    return filterCriteria;
  }

  clearActiveFilterItems() {
    this.setState({
      ...this.state,
      activeFilterItems: []
    });

    const criteria = this.buildFilterCriteria([], this.state.activePrimaryFilterItems);
    this.initialData(criteria);

    this.getFilterCategoryModelStatics(criteria);
  }

  getFilterCategoryModelStatics(criteria) {
    if (this.props.enabledFilter) {
      if (this.props.groupByFilter !== '') {
        criteria.push(this.getGroupByFilterValues());
      }
      ReactItemListingApi.getFilterCategoryModelStatics({
        paths: [this.props.categoryId],
        includedContentIds: this.props.includedContentIds,
        defaultCriteria: this.props.defaultFilteringBy,
        groupByFilter: this.props.groupByFilter,
        filterBy: this.props.filterBy,
        criteria: criteria
      }, this.props.isGroupBySubCategory, this.state.activePrimaryFilterCategory)
        .then(stasResponsive => {
          this.setState({
            ...this.state,
            filterCategories: stasResponsive.data,
            isStaticsLoaded: true
          });
        });
    }
  }

  getDefaultFilterCategory(criteria) {
    return new Promise((resolve, reject) => {
      if (this.props.enabledFilter) {
        ReactItemListingApi.getFilterCategoryModelStatics({
          parentId: this.props.categoryId,
          includedContentIds: this.props.includedContentIds,
          defaultCriteria: this.props.defaultFilteringBy,
          groupByFilter: this.props.groupByFilter,
          criteria: criteria
        }, this.props.isGroupBySubCategory, this.props.filterBy, this.state.activePrimaryFilterCategory)
          .then(stasResponsive => {
            resolve(stasResponsive.data);
          });
      } else {
        reject();
      }
    });
  }

  getGroupByFilterValues() {
    const filterCategories = [...this.state.filterCategories];
    const groupingFilterCategory = filterCategories.find(filter => filter.key === this.props.groupByFilter);
    if (groupingFilterCategory && groupingFilterCategory.items.length > 0) {
      return this.buildFilterCriteria(groupingFilterCategory.items, []);
    }

    return [];
  }

  onClickCloseModal() {
    this.setState({
      ...this.state,
      expandedFilterCategories: [],
      showModal: false
    });
  }

  onClickOpenModal() {
    this.setState({
      ...this.state,
      expandedFilterCategories: [],
      showModal: true
    });
  }

  reAdjustCaptionHeight() {
    const maxHeight = Math.max.apply(null, $(".react-app-root p.responsive-caption").map(function () {
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
    return (
      <React.Fragment>
        {this.props.enabledFilter &&
          <FilterToolbar
            filterCategories={this.state.filterCategories}
            activeFilterItem={this.state.activeFilterItem}
            onClickFilterItem={this.onClickFilterItem} />
        }
        {this.state.isLoaded ? <ContentRowItems
          items={this.state.items}
          itemsPerPage={parseInt(this.props.itemsPerPage, 10)}
          itemView={this.props.itemView}
          isGroupBySubCategory={this.props.isGroupBySubCategory}
          activePrimaryFilterCategory={this.state.activePrimaryFilterCategory}
          activeFilterItems={this.state.activeFilterItems}
          viewDetailsLabel={this.props.viewDetailsLabel}
          captionHeight={this.state.captionHeight}
          isHideViewAll={this.props.isGroupBySubCategory && this.props.groupByFilter !== ''}
        /> : <Spinner />
        }

        {!this.state.isLastPage && this.state.items.length > 0 &&
          <ShowMoreButton title={this.props.viewMoreLabel} onClick={this.onClickShowMore} isClickShowMore={this.state.isClickShowMore} />
        }
      </React.Fragment>
    );
  }
}

ResponsiveItemListingPageContainer.propTypes = {
  categoryId: PropTypes.string,
  itemsPerPage: PropTypes.number,
  isGroupBySubCategory: PropTypes.bool,
  itemView: PropTypes.string,
  filterBy: PropTypes.array,
  primaryFilterIds: PropTypes.array,
  enabledFilter: PropTypes.bool,
  includedContentIds: PropTypes.array,
  sortBy: PropTypes.string,
  orderType: PropTypes.string,
  contentType: PropTypes.string,
  defaultFilteringBy: PropTypes.array,
  viewMoreLabel: PropTypes.string,
  viewDetailsLabel: PropTypes.string,
  groupByFilter: PropTypes.string
};

export default ResponsiveItemListingPageContainer;