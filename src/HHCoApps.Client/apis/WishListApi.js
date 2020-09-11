import axios from 'axios';

class WishListApi {
  static sendWishListEmail(wishList) {
    return axios.post(`${AppConfig.apis.sendWishListEmail}`, wishList);
  }
}

export default WishListApi;