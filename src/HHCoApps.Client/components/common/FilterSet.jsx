import React from 'react';

import { FILTER_CATEGORY_TYPE, FILTER_ITEM_TYPE } from "constants/EntityTypes";
import PropTypes from 'prop-types';

class FilterSet extends React.Component {
  constructor(props) {
    super(props);
    this.renderFilterItem = this.renderFilterItem.bind(this);
  }

  renderFilterItem(filterItem) {
    return (
      <li className={this.props.activeFilterItem === filterItem.key ? 'active' : ''} key={filterItem.key}>
        <a onClick={() => this.props.onClick(filterItem)}>{filterItem.name}</a>
      </li>
    );
  }

  render() {
    return (this.props.filterCategory.items.map(item => this.renderFilterItem(item)));
  }
}

FilterSet.propTypes = {
  filterCategory: FILTER_CATEGORY_TYPE,
  onClick: PropTypes.func,
  activeFilterItem: PropTypes.string,
  displayCount: PropTypes.bool
};

export default FilterSet;