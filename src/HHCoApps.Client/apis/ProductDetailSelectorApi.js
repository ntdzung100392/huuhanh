import axios from 'axios';

class ProductDetailSelectorApi {
  static getProductDetailSelector(contentId) {
    return axios.get(`${AppConfig.apis.getProductDetailSelectorUrl}?id=${contentId}`);
  }

  static getProductDetailInfo(wishListItemRequest, viewMode) {
    return axios.post(`${AppConfig.apis.getProductDetailInfo}?viewMode=${viewMode}`, wishListItemRequest);
  }

  static getProductDetailsByIds(wishListItemsRequest, viewMode) {
    return axios.post(`${AppConfig.apis.getProductDetailsByIds}?viewMode=${viewMode}`, wishListItemsRequest);
  }
}

export default ProductDetailSelectorApi;