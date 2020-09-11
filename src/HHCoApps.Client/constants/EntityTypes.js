import propTypes from 'prop-types';

export const SEARCH_ITEM_TYPE = propTypes.shape({
  id: propTypes.string,
  title: propTypes.string,
  url: propTypes.string,
  imageUrl: propTypes.string,
  imageAlt: propTypes.string,
  caption: propTypes.string
});

export const FILTER_ITEM_TYPE = propTypes.shape({
  key: propTypes.string,
  name: propTypes.string
});

export const FILTER_CATEGORY_TYPE = propTypes.shape({
  key: propTypes.string,
  name: propTypes.string,
  items: propTypes.arrayOf(FILTER_ITEM_TYPE)
});

export const STORE_ADDRESS = propTypes.shape({
  suburb: propTypes.string,
  postCode: propTypes.string,
  phone: propTypes.string,
  formattedAddress: propTypes.string
});

export const STORE_PROPERTIES = propTypes.shape({
  storeId: propTypes.string,
  name: propTypes.string,
  website: propTypes.string,
  distanceFromOrigin: propTypes.number,
  address: STORE_ADDRESS,
  storeUrl: propTypes.string
});

export const STORE_GEOMETRY = propTypes.shape({
  type: propTypes.string,
  coordinates: propTypes.arrayOf(propTypes.number)
});

export const STORE_DETAIL = propTypes.shape({
  properties: STORE_PROPERTIES,
  geometry: STORE_GEOMETRY,
  type: propTypes.string
});

export const GEO_JSON = propTypes.shape({
  type: propTypes.string,
  features: propTypes.arrayOf(propTypes.shape({
    properties: STORE_PROPERTIES,
    geometry: STORE_GEOMETRY,
    type: propTypes.string
  }))
});

export const PRODUCT_COAT = propTypes.shape({
  coatOption: propTypes.string,
  colorUid: propTypes.string,
  colorImageUrl: propTypes.string
});

export const PRODUCT_TIMBER = propTypes.shape({
  timberName: propTypes.string,
  timberUid: propTypes.string,
  coats: propTypes.arrayOf(PRODUCT_COAT)
});

export const PRODUCT_COLOR = propTypes.shape({
  colorImageUrl: propTypes.string,
  colorName: propTypes.string,
  colorUid: propTypes.string,
  timbers: propTypes.arrayOf(PRODUCT_TIMBER)
});

export const MINI_WISHLIST_ITEM = propTypes.shape({
  id: propTypes.string,
  title: propTypes.string,
  size: propTypes.string,
  colorName: propTypes.string,
  colorImageUrl: propTypes.string,
  productUri: propTypes.string,
  productImageUrl: propTypes.string,
  productImageAlt: propTypes.string,
  caption: propTypes.string,
  quantity: propTypes.number
});

export const EMAIL_FORM_ERROR_FIELDS = propTypes.shape({
  firstName: propTypes.string,
  lastName: propTypes.string,
  email: propTypes.string
});

export const LINK = propTypes.shape({
  name: propTypes.string,
  target: propTypes.string,
  type: propTypes.number,
  url: propTypes.string,
  udi: propTypes.string
});

export const PRODUCT_MENU_ITEM = propTypes.shape({
  key: propTypes.string,
  link: LINK,
  hasChildItems: propTypes.bool,
  title: propTypes.string,
  numberOfChildItemsPerColumn: propTypes.number,
  childItems: propTypes.array
});

export const PRODUCT_NAVIGATION = propTypes.shape({
  viewAllLink: propTypes.string,
  viewAllLinkTarget: propTypes.string,
  viewAllLabel: propTypes.string,
  nodeChildrens: propTypes.arrayOf(PRODUCT_MENU_ITEM)
});

export const RELATED_PRODUCT = propTypes.shape({
  title: propTypes.string,
  productSubCategory: propTypes.string,
  color: propTypes.string,
  productUrl: propTypes.string
});

export const PRODUCT_DETAIL = propTypes.shape({
  caption: propTypes.string,
  id: propTypes.string,
  imageAlt: propTypes.string,
  imageHeight: propTypes.number,
  imageUrl: propTypes.string,
  imageWidth: propTypes.number,
  navigationTitle: propTypes.string,
  parentTitle: propTypes.string,
  relatedProducts: propTypes.arrayOf(RELATED_PRODUCT),
  subTitle: propTypes.string,
  target: propTypes.string,
  title: propTypes.string,
  udi: propTypes.string,
  url: propTypes.string,
});

export const PRODUCT_VARIANT = propTypes.shape({
  id: propTypes.string,
  image: propTypes.shape({
    src: propTypes.string,
    altText: propTypes.string
  }),
  price: propTypes.string,
  available: propTypes.bool,
  sku: propTypes.string,
  title: propTypes.string,
  selectedOptions: propTypes.arrayOf(propTypes.shape({
    name: propTypes.string,
    value: propTypes.string
  }))
});

export const SHOPPING_LINE_ITEM = propTypes.shape({
  id: propTypes.string,
  title: propTypes.string,
  quantity: propTypes.number,
  variant: PRODUCT_VARIANT
});