import React from 'react';
import PropTypes from 'prop-types';
import { PRODUCT_MENU_ITEM, PRODUCT_NAVIGATION } from 'constants/EntityTypes';

function ProductNavigationStep(props) {
    const { step, menuItem, getProductList, products, activeRootMenu, getProductDetail } = props;

    const renderStepOne = () => {
        return (
            <div className="filter-menu__body filter-menu__body--first" id="step-1">
                <div className="filter-menu__column">
                    {
                        menuItem.nodeChildrens.map((menu, index) => (
                            <div className="filter-menu__block" key={index}>
                                <div className="is-flex justify-content-between align-items-center">
                                    <h3 className="heading-level-3 text-capitalize dashed">
                                        <a href={menu.link.url} target="_blank">{menu.title}</a> 
                                    </h3>
                                    <div className="links">
                                        <a href={menu.link.url} target="_blank">
                                            <i className="icon-arrow-right-circle text-yellow"/>
                                        </a>
                                    </div>
                                </div>
                                <hr />
                                <div className="nav-links">
                                    <ul className="nav-list">
                                        {menu.hasChildItems && menu.childItems.map((submenu, index) => {
                                            return (
                                                <li key={index}><a onClick={() => getProductList(submenu)} className="text-capitalize"
                                                    data-target={"#" + submenu.key} data-toggle="collapse"
                                                >{submenu.title}</a></li>
                                            )
                                        })}
                                    </ul>
                                </div>
                            </div>
                        ))
                    }
                    <div className="text-right view-all-products">
                        <a href={menuItem.viewAllLink} target={menuItem.viewAllLinkTarget} className="next-step is-flex align-items-center justify-content-end">
                            <span className="text-uppercase">{menuItem.viewAllLabel}</span>
                            <i className="icon-arrow-right-circle" />
                        </a>
                    </div>
                </div>
            </div>
        )
    }

    const renderStepTwo = () => {
        return (
            <React.Fragment>
                {activeRootMenu && Object.values(products).length > 0 && (
                    <div className="filter-menu__body filter-menu__body--secondary" aria-expanded="false" id={activeRootMenu.key}>
                        <a className="visible-xs hidden-md prev-step" data-toggle="collapse" data-target={"#" + activeRootMenu.key}>
                            <i className="icon-left-arrow" />
                        </a>
                        <div className="filter-menu__column">
                            <div>
                                <div className="filter-menu__block">
                                    <div className="is-flex justify-content-between align-items-center">
                                        <h3 className="heading-level-3 text-capitalize dashed">
                                            <a href={activeRootMenu.link.url} target="_blank">{activeRootMenu.title}</a>
                                        </h3>
                                        <div>
                                            <a href={activeRootMenu.link.url} target="_blank">
                                                <i className="icon-arrow-right-circle text-yellow" />
                                            </a>
                                        </div>
                                    </div>
                                    <hr />
                                    <div className="nav-links">
                                        <ul className="nav-list">
                                            {
                                                Object.values(products).length > 0 && products.map((item, index) => (
                                                    <li key={index}><a onClick={() => getProductDetail(item)} className="text-capitalize" data-toggle="collapse" data-target={"#" + item.key}>
                                                        {item.title}</a></li>
                                                ))
                                            }
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </React.Fragment>
        )
    }

    return (
        <React.Fragment>
            {(step == 1) ? renderStepOne() : renderStepTwo()}
        </React.Fragment>
    )
}

ProductNavigationStep.propTypes = {
    step: PropTypes.number,
    menuItem: PRODUCT_NAVIGATION,
    getProductList: PropTypes.func,
    getProductDetail: PropTypes.func,
    activeRootMenu: PRODUCT_MENU_ITEM,
    products: PropTypes.arrayOf(PRODUCT_MENU_ITEM)
}

export default ProductNavigationStep