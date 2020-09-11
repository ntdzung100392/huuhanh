import React from 'react';
import Slider from "react-slick";
import PropTypes from 'prop-types';
import ContentInfo from './ContentInfo';
import { STRING_RESOURCES } from "constants/AppConstants";
import { SEARCH_ITEM_TYPE } from "constants/EntityTypes";

class ContentInfoList extends React.Component {
  constructor(props) {
    super(props);
    this.renderElementsByRow = this.renderElementsByRow.bind(this);
  }

  renderElementsByRow() {
    var elements = this.props.items.map(
      item => <ContentInfo key={item.id} item={item} itemView={this.props.itemView} isCarouselViewMode={this.props.isCarouselViewMode} captionHeight={this.props.captionHeight} />
      );
    var rowElements = [];
    var elementsInRow = [];
    const sliderSettings = {
      dots: false,
      infinite: true,
      slidesToShow: 3,
      slidesToScroll: 1,
      arrows: true,
      responsive: [
        {
          breakpoint: 1024,
          settings: {
            slidesToShow: 3,
            slidesToScroll: 3,
            infinite: true
          }
        },
        {
          breakpoint: 768,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2,
            arrows: false,
            infinite: true
          }
        },
        {
          breakpoint: 480,
          settings: {
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            infinite: true
          }
        }
      ]
    };

    for (var index = 0; index < elements.length; index++) {
      elementsInRow.push(elements[index]);
    }

    var rowElement = (
      this.props.isCarouselViewMode ?
        <div className="row" key={Math.random()}>
          <Slider {...sliderSettings}>{elementsInRow}</Slider>
        </div> :
        <div className="row display-flex" key={Math.random()}>
          {elementsInRow}
        </div>
    );

    rowElements.push(rowElement);

    return rowElements;
  }

  render() {
    if (this.props.items.length > 0) {
      const rowElements = this.renderElementsByRow();

      return (
        <div className="search-article-gallery filter-results" key={Math.random()}>
          {rowElements}
        </div>
      );
    }

    return (
      <div className="search-article-gallery filter-results">
        <div className="row">
          <div style={{ textAlign: 'center' }}>
            {STRING_RESOURCES.noItemsToDisplay}
          </div>
        </div>
      </div>
    );
  }
}

ContentInfoList.propTypes = {
  itemView: PropTypes.string,
  items: PropTypes.arrayOf(SEARCH_ITEM_TYPE),
  captionHeight: PropTypes.number,
  isCarouselViewMode: PropTypes.bool
};

export default ContentInfoList;