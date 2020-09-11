import React from 'react';
import ContentColumnItem from './ContentColumnItem';
import PropTypes from 'prop-types';
import { STRING_RESOURCES } from "constants/AppConstants";
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";
import { CONTENT_LISTING_LAYOUTS } from "constants/responsiveItemListing/ResponsiveReactConstant";

class ContentRowItems extends React.Component {
  constructor(props) {
    super(props);

    this.renderTwoColumns = this.renderTwoColumns.bind(this);
    this.renderThreeColumnsWithFlexibleHeightItems = this.renderThreeColumnsWithFlexibleHeightItems.bind(this);
    this.renderFourColumns = this.renderFourColumns.bind(this);
    this.renderItemsRow = this.renderItemsRow.bind(this);
    this.getQueryString = this.getQueryString.bind(this);
  }

  renderItemsRow() {
    return this.props.items.map(item =>
      <ContentColumnItem key={item.id} item={item} viewDetailsLabel={this.props.viewDetailsLabel} itemView={this.props.itemView} captionHeight={this.props.captionHeight} />
    );
  }

  renderTwoColumns(itemsRow) {
    return (
      <div className="project-list">
        <div className="row display-flex">
          {itemsRow}
        </div>
      </div>
    );
  }

  renderThreeColumnsWithFlexibleHeightItems(itemsRow) {
    return (
      <div className="blog-list">
        {itemsRow}
      </div>
    )

  }

  renderThreeColumnsWithAttributes(itemsRow) {
    return (
      <div className="get-the-look-category__result">
        <div className="row display-flex">
          {itemsRow}
        </div>
      </div>
    );
  }

  renderFourColumns(itemsRow) {
    return (
      <div className="row display-flex" id="filter-result">
        {itemsRow}
      </div>
    );
  }

  getQueryString(activePrimaryFilter, activeFilterItems) {
    let activeFiltersQuery = [];

    if (activePrimaryFilter || (activeFilterItems && activeFilterItems.length > 0)) {
      activeFiltersQuery.push('?');

      if (activePrimaryFilter) {
        activeFiltersQuery.push('primaryFilter=');
        activeFiltersQuery.push(activePrimaryFilter);
      }

      if (activeFilterItems) {
        const subPrimaryFilterItems = activeFilterItems.filter(x => x.isPrimarySubFilter);
        if (subPrimaryFilterItems && subPrimaryFilterItems.length > 0) {
          activeFiltersQuery.push('&subPrimary=');
          subPrimaryFilterItems.forEach(subPrimary => {
            activeFiltersQuery.push(`${subPrimary.name}`);
            if (subPrimaryFilterItems.indexOf(subPrimary) < subPrimaryFilterItems.length - 1) {
              activeFiltersQuery.push(',');
            }
          });
        }

        const filterItems = activeFilterItems.filter(x => !x.isPrimarySubFilter).map(item => ({
          ...item,
          key: item.key.split(':')[0],
        }));

        const groupedFilterItems = filterItems.reduce((grouped, array) => {
          grouped[array.key] = [...grouped[array.key] || [], array];
          return grouped;
        }, {});

        const filterKeys = Object.keys(groupedFilterItems);
        filterKeys.forEach(key => {
          if (filterKeys.indexOf(key) === 0) {
            activeFiltersQuery.push(`&filterBy=${key}:`);
          }
          else {
            activeFiltersQuery.push(`;${key}:`);
          }

          const filterValues = groupedFilterItems[key].map(filter => filter.name);
          filterValues.forEach(value => {
            activeFiltersQuery.push(`${value}`);
            if (filterValues.indexOf(value) < filterValues.length - 1) {
              activeFiltersQuery.push(',');
            }
          });
        });
      }
    }

    return activeFiltersQuery.join('');
  }

  render() {
    if (this.props.items.length > 0) {
      const itemsRow = this.renderItemsRow();
      if (itemsRow) {
        switch (this.props.itemView) {
          case CONTENT_LISTING_LAYOUTS.twoColumn:
            return this.renderTwoColumns(itemsRow);
          case CONTENT_LISTING_LAYOUTS.threeColumnsWithFlexibleHeightItems:
            return this.renderThreeColumnsWithFlexibleHeightItems(itemsRow);
          case CONTENT_LISTING_LAYOUTS.threeColumnsWithAttributes:
            return this.renderThreeColumnsWithAttributes(itemsRow);
          case CONTENT_LISTING_LAYOUTS.fourColumns:
            return this.renderFourColumns(itemsRow);
          default:
            return this.renderTwoColumns(itemsRow);
        }
      }
    }

    return (
      <div className="article-gallery">
        <div className="row">
          <div style={{ textAlign: 'center' }}>
            {STRING_RESOURCES.noItemsToDisplay}
          </div>
        </div>
      </div>
    );
  }
}

ContentRowItems.propTypes = {
  items: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  itemsPerPage: PropTypes.number,
  itemView: PropTypes.string,
  activePrimaryFilterCategory: PropTypes.string,
  activeFilterItems: PropTypes.array,
  isGroupBySubCategory: PropTypes.bool,
  isHideViewAll: PropTypes.bool,
  captionHeight: PropTypes.number,
  viewDetailsLabel: PropTypes.string
};

export default ContentRowItems;