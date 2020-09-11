import axios from 'axios';

class ReactItemListingApi {
  static getFilterCategoryModel(filterBy, activePrimaryFilterCategory, groupByFilter) {
    return axios.get(`${AppConfig.apis.getFilterCategoryModel}?activePrimaryFilterCategory=${activePrimaryFilterCategory}&groupByFilter=${groupByFilter}`, {
      params: {
        filterBy
      }
    });
  }

  static getFilterCategoryModelStatics(searchRequest, isGrandParent, activePrimaryFilterCategory) {
    return axios.post(`${AppConfig.apis.getFilterCategoryModelStatics}?isGrandParent=${isGrandParent}&activePrimaryFilterCategory=${activePrimaryFilterCategory}`, searchRequest);
  }

  static getPrimaryFilterCategoryModels(primaryFilters) {
    return axios.get(`${AppConfig.apis.getPrimaryFilterCategoryModels}`, {
      params: {
        primaryFilters
      }
    });
  }
}

export default ReactItemListingApi;