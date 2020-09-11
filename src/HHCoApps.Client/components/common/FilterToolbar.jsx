import React from 'react';
import { FILTER_CATEGORY_TYPE, FILTER_ITEM_TYPE } from 'constants/EntityTypes';
import PropTypes from 'prop-types';
import FilterSet from 'commonComponents/FilterSet';

class FilterToolbar extends React.Component {
  constructor(props) {
    super(props);
    this.renderFilterSets = this.renderFilterSets.bind(this);
  }

  renderFilterSets() {
    return this.props.filterCategories.map(filterCategory => {
      return (
        <FilterSet
          key={filterCategory.key}
          activeFilterItem={this.props.activeFilterItem}
          onClick={this.props.onClickFilterItem}
          filterCategory={filterCategory} />
      );
    });
  }

  render() {
    if (this.props.filterCategories.length > 0) {
      return (
        <React.Fragment>
          <div className="product-list__filter">
            <div className="container visible-xs visible-sm hidden-lg hidden-md">
              <div className="panel-group">
                <div className="panel-heading">
                  <h4 className="panel-title">
                    <a onClick={() => this.props.onClickFilterItem()} className="text-uppercase" data-toggle="collapse" data-target="#blogFilter">All</a>
                  </h4>
                </div>
                <div id="blogFilter" className="panel-collapse collapse">
                  <div className="panel-body">
                    <ul>
                      {this.renderFilterSets()}
                    </ul>
                  </div>
                </div>
              </div>
            </div>
            <div className="blog-nav visible-md visible-lg hidden-xs hidden-sm px-0">
              <ul className="blog-nav__desktop">
                <li className={this.props.activeFilterItem === '' ? 'active' : ''} ><a onClick={() => this.props.onClickFilterItem()}>All</a></li>
                {this.renderFilterSets()}
              </ul>
            </div>
          </div>
        </React.Fragment >
      );
    }

    return (<div className="blog-nav"></div>);
  }
}

FilterToolbar.propTypes = {
  filterCategories: PropTypes.arrayOf(FILTER_CATEGORY_TYPE),
  activeFilterItem: PropTypes.string,
  onClickFilterItem: PropTypes.func
};

export default FilterToolbar;