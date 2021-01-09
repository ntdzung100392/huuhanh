import React, { Component } from "react";
import { STRING_RESOURCES, CUSTOM_EVENT_NAME, MONEY_FORMAT } from 'constants/AppConstants';
import ProductColorSelector from "./productColorSelector";
import ProductTimberSelector from "./productTimberSelector";
import ProductSizes from "./ProductSizes";
import ProductDetailSelectorApi from "apis/ProductDetailSelectorApi";
import queryString from "query-string";
import UrlHelper from "helpers/UrlHelper";
import PropTypes from "prop-types";
import Spinner from 'commonComponents/Spinner';
import { ShopifyContext } from 'services/shopify-context';
import * as numeral from 'numeral';

class ProductDetailSelectorContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isLoaded: false,
      allSizes: [],
      availableSizes: [],
      availableColors: [],
      allColors: [],
      activeColor: {},
      activeTimbers: [],
      selectedTimber: {},
      selectedSize: '',
      timberCaption: '',
      colorCaption: '',
      shopify: {},
      shopifyMappings: []
    };
    this.stringifyQuery = this.stringifyQuery.bind(this);
    this.parseQueryString = this.parseQueryString.bind(this);
    this.setActiveColor = this.setActiveColor.bind(this);
    this.setActiveTimber = this.setActiveTimber.bind(this);
    this.findMatchingTimbers = this.findMatchingTimbers.bind(this);
    this.setActiveSize = this.setActiveSize.bind(this);
    this.getShopifyData = this.getShopifyData.bind(this);
    this.renderVariant = this.renderVariant.bind(this);
  }

  componentDidMount() {
    ProductDetailSelectorApi.getProductDetailSelector(this.props.contentId).then(
      ({ data }) => {
        this.setState({
          allSizes: data.sizes,
          allColors: data.colors,
          timberCaption: data.timberCaption,
          colorCaption: data.colorCaption,
          shopifyMappings: data.shopifyMappings
        });
        this.parseQueryString();
        this.getShopifyData(this.props.productTitle);
      });
  }

  findMatchingTimbers(colorUid) {
    const activeColor = Object.values(this.state.availableColors).find(
      (x) => x.colorUid === colorUid
    );

    if (activeColor) {
      this.setState({
        activeColor: activeColor,
        activeTimbers: activeColor.timbers,
        selectedTimber: activeColor.timbers ? activeColor.timbers[0] : {},
        ...this.getSizesAndSelectedSizeByColor(activeColor, this.state.allSizes, this.state.selectedSize),
      });
    }
  }

  parseQueryString() {
    let activeColor, selectedSize;
    const parsedQuery = queryString.parse(window.location.search);
    let newState = { ... this.state, availableSizes: this.state.allSizes, availableColors: this.state.allColors };

    if (Object.keys(parsedQuery).length > 0) {
      activeColor = Object.values(this.state.allColors).find(x => UrlHelper.slugify(x.colorName) === UrlHelper.slugify(parsedQuery.selectedColor));
      selectedSize = Object.values(this.state.allSizes).find(x => UrlHelper.slugify(x) === UrlHelper.slugify(parsedQuery.selectedSize));
    }

    if (activeColor) {
      newState = { ...newState, ... this.getSizesAndSelectedSizeByColor(activeColor, this.state.allSizes, selectedSize), activeColor };
    }
    else {
      selectedSize = selectedSize ? selectedSize : (Object.keys(this.state.allSizes).length > 0 ? Object.values(this.state.allSizes)[0] : {});
      newState = { ...newState, ... this.getColorsAndActiveColorBySize(selectedSize, this.state.allColors), selectedSize };
    }

    this.setState({
      ...newState,
      activeTimbers: newState.activeColor ? newState.activeColor.timbers : [],
      selectedTimber: newState.activeColor && newState.activeColor.timbers ? newState.activeColor.timbers[0] : {},
      isLoaded: true
    });
  }

  stringifyQuery(color) {
    if (window.history.pushState) {
      let searchParams = new URLSearchParams(window.location.search);
      searchParams.set("selectedColor", color);
      let newUrl = window.location.origin + window.location.pathname + "?" + searchParams.toString();
      window.history.pushState({ path: newUrl }, "", newUrl);
    }
  }

  setActiveColor(color) {
    this.findMatchingTimbers(color.colorUid);
    this.stringifyQuery(color.colorName);
  }


  setActiveTimber(timber) {
    const timberIndex = this.state.activeTimbers.findIndex(x => x.timberUid === timber.timberUid);
    if (timberIndex > -1) {
      this.setState({
        selectedTimber: timber
      });
    }
  }

  setActiveSize(value) {
    if (value) {
      this.setState({
        selectedSize: value,
        ...this.getColorsAndActiveColorBySize(value, this.state.allColors, this.state.activeColor)
      });
    } else {
      this.setState({
        selectedSize: value
      });
    }
  }

  getSizesAndSelectedSizeByColor(color, allSizes, defaultSize) {
    let sizes = [...allSizes];

    if (color.isSizeOverride) {
      let removeItems = [];

      sizes.map((item, index) => {
        if (color.availableSizes.findIndex(it => it == item) == -1) {
          removeItems.push(index);
        }
      });

      if (removeItems.length > 0) {
        const indexSet = new Set(removeItems);
        sizes = sizes.filter((value, i) => !indexSet.has(i));
      }
    }

    if (sizes.length > 0) {
      let selectedSize = sizes[0];
      if (defaultSize) {
        let index = sizes.findIndex(it => it == defaultSize);
        if (index > -1) {
          selectedSize = sizes[index];
        }
      }

      return { selectedSize: selectedSize, availableSizes: sizes };
    }

    return { selectedSize: {}, availableSizes: [] };
  }

  getColorsAndActiveColorBySize(size, allColors, defaultColor) {
    if (allColors.length == 0)
      return { activeColor: {}, availableColors: [] };

    let availableColors = [];
    let activeColor = {};

    for (let i = 0; i < allColors.length; i++) {
      let color = allColors[i];
      if (color.isSizeOverride) {
        if (color.availableSizes.findIndex(it => it == size) > -1) {
          availableColors.push(color);
        }
      }
      else {
        availableColors.push(color);
      }
    }

    if (availableColors.length > 0) {
      activeColor = availableColors[0];
      if (defaultColor) {
        let index = availableColors.findIndex(it => it.colorName == defaultColor.colorName);
        if (index > -1) {
          activeColor = availableColors[index];
        }
      }
    }

    return { activeColor: activeColor, availableColors: availableColors };
  }

  renderVariant(color, size) {
    const { shopify, shopifyMappings } = this.state;
    const sizeColor = `${size}-${color.colorName || ''}`;
    let template = null;
    let selectedVariant = {};

    if (shopify.variants && shopify.variants.length > 0 && shopifyMappings.length > 0) {
      const validItem = shopifyMappings.find(mappingItem => mappingItem.sizeColor === sizeColor);

      if (validItem) {
        selectedVariant = shopify.variants.find(v => v.sku === validItem.sku);

        if (selectedVariant) {
          template = (
            <div className="product-detail__shopify-product-variant">
              <span className="product-variant-id">{selectedVariant.id}</span>
              <span className="product-variant-sku">{selectedVariant.sku}</span>
              <span className="product-variant-price">{selectedVariant.price}</span>
            </div>
          );
        }
      }
    }

    if (template) {
      document.dispatchEvent(new CustomEvent(CUSTOM_EVENT_NAME.selectedProductVariantUpdate, {
        detail: {
          ...selectedVariant,
          displayPrice: numeral(selectedVariant.price).format(MONEY_FORMAT)
        }
      }));
    } else {
      document.dispatchEvent(new CustomEvent(CUSTOM_EVENT_NAME.selectedProductVariantUpdate));
    }

    return template;
  }

  getShopifyData(productTitle) {
    if (this.context && this.context.client) {
      this.context.client.searchProductByTitle([productTitle], 2).then(shopifyProducts => {
        if (shopifyProducts.length > 0) {
          // Support duplicate product title
          const product = shopifyProducts[0];
          product.variants = shopifyProducts.reduce((acc, p) => {
            acc = acc.concat(p.variants);

            return acc;
          }, []);

          this.setState({
            ...this.state,
            shopify: product
          });
        }
      });
    }
  }

  render() {
    const parsedQuery = queryString.parse(window.location.search);
    if (!this.state.isLoaded)
      return (<Spinner />);

    return (
      <React.Fragment>
        {this.state.availableSizes.length > 0 && (
          <div className="row product-detail__mobile-wrapper">
            <ProductSizes sizes={this.state.availableSizes} selectedSize={this.state.selectedSize} setActiveSize={this.setActiveSize} />
          </div>
        )
        }
        {(Object.keys(this.state.activeColor).length > 0 || Object.keys(parsedQuery).length > 0) && (
          <div className="row product-detail__selections product-detail__mobile-wrapper">
            <ProductColorSelector
              colors={this.state.availableColors}
              activeColor={this.state.activeColor}
              setActiveColor={this.setActiveColor}
              productTitle={this.props.productTitle}
              colorCaption={this.state.colorCaption}
            />
            <ProductTimberSelector
              timberCaption={this.state.timberCaption ? this.state.timberCaption : STRING_RESOURCES.timberCaption}
              activeTimbers={this.state.activeTimbers}
              setActiveTimber={this.setActiveTimber}
              selectedTimber={this.state.selectedTimber}
            />
          </div>
        )}
        {
          (this.state.shopify.variants) && (
            this.renderVariant(this.state.activeColor, this.state.selectedSize)
          )
        }

      </React.Fragment>
    );
  }
}
ProductDetailSelectorContainer.propTypes = {
  contentId: PropTypes.string,
  productTitle: PropTypes.string
};

ProductDetailSelectorContainer.contextType = ShopifyContext;

export default ProductDetailSelectorContainer