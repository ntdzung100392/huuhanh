import React, { Component } from 'react';
import PropTypes from 'prop-types';
import ProductNavigationApi from 'apis/ProductNavigationApi';
import ProductNavigationStep from './productNavigationStep';
import ProductNavigationDetailStep from './productNavigationDetailStep';

class ProductNavigationContainer extends Component {
  constructor(props) {
    super(props)

    this.state = {
      menuItem: {},
      products: [],
      activeRootMenu: {},
      activeProduct: {}
    };
    this.getProductList = this.getProductList.bind(this);
    this.getProductDetail = this.getProductDetail.bind(this);
    this.toggleMenu = this.toggleMenu.bind(this);
  }

  componentDidMount() {
    ProductNavigationApi.getMenuItems(this.props.contentId)
      .then(({ data }) => {
        this.setState({
          menuItem: data
        });
      });
  }

  getProductList(submenu) {
    if (submenu) {
      this.setState({
        activeRootMenu: submenu
      }, () => {
        if (submenu.hasChildItems) {
          ProductNavigationApi.getProductList(submenu.key)
            .then(({ data }) => {
              this.setState({
                products: data,
              }, () => {
                if($(`#${submenu.key}`).attr("aria-expanded") == "false"){
                  $(`#${submenu.key}`).collapse('show');
                }
                if ($(`#${submenu.key}`).next().attr("aria-expanded") == "true") {
                  const thirdStep = $(`#${submenu.key}`).next().attr("id");
                  $(`#${thirdStep}`).collapse('hide');
                }
              });
            });
        } else {
          this.getProductDetail(submenu);
        }
      })
    }
  }

  getProductDetail(product) {
    if (product) {
      ProductNavigationApi.getProductDetail(product.key)
        .then(({ data }) => {
          this.setState({
            activeProduct: data
          }, () => {
            $("#" + product.key).collapse('show');
          });
        })
    }
  }

  toggleMenu() {
    this.setState({
      products: [],
      activeRootMenu: {},
      activeProduct: {}
    }, () => {
      $("#filter-menu").collapse();
    });
  }

  render() {
    return (
      <div className="product-navigation-menu">
        <div id="filter-menu" className="filter-menu" aria-expanded="false">
          <div className="visible-md visible-lg filter-menu__header--fixed">
            <div className="filter-menu__header text-white hidden-xs">
              <div className="filter-menu__header--left is-flex align-items-center">
                <i className="icon-filter" />
                <span className="text-uppercase"><strong>Quick product filter</strong></span>
              </div>
              <div className="filter-menu__header--right">
                <a data-toggle="collapse" onClick={() => this.toggleMenu()} data-target="#filter-menu" id="close-btn">
                  <i className="icon-close" />
                </a>
              </div>
            </div>
          </div>
            <div className="is-flex flex-wrap" style={{ height: '100%' }}>
              {
                Object.values(this.state.menuItem).length > 0 &&
                <ProductNavigationStep
                  step={1}
                  menuItem={this.state.menuItem}
                  getProductList={this.getProductList}
                  getProductDetail={this.getProductDetail}
                />
              }
              {
                Object.values(this.state.products).length > 0 && this.state.activeRootMenu.hasChildItems &&
                <ProductNavigationStep
                  step={2}
                  menuItem={this.state.menuItem}
                  products={this.state.products}
                  activeRootMenu={this.state.activeRootMenu}
                  getProductDetail={this.getProductDetail}
                />
              }
              <ProductNavigationDetailStep activeProduct={this.state.activeProduct} activeRootMenu={this.state.activeRootMenu} />
            </div>
        </div>
      </div>

    )
  }
}

ProductNavigationContainer.propTypes = {
  contentId: PropTypes.string
}

export default ProductNavigationContainer;