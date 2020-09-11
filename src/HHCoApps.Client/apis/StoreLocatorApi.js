import axios from 'axios';

class StoreLocatorApi {
  static getMapSettings(contentID) {
    return axios.get(`${AppConfig.apis.getMapSettings}?id=${contentID}`);
  }

  static searchNearbyStores(lat, long, contentID, numberOfStoresForDisplay) {
    return axios.get(`${AppConfig.apis.searchNearbyStores}?latitude=${lat}&longitude=${long}&id=${contentID}&numberOfStoresForDisplay=${numberOfStoresForDisplay}`);
  }
}

export default StoreLocatorApi;