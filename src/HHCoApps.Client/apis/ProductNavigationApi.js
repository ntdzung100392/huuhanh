import axios from 'axios';

class ProductNavigationApi{
    static getMenuItems(contentId){
        return axios.get(`${AppConfig.apis.getMenuItemsUrl}?contentId=${contentId}`);
    }
    static getProductList(contentId){
        return axios.get(`${AppConfig.apis.getProductListUrl}?contentId=${contentId}`)
    }
    static getProductDetail(contentId){
        return axios.get(`${AppConfig.apis.getProductDetailUrl}?contentId=${contentId}`)
    }
}

export default ProductNavigationApi