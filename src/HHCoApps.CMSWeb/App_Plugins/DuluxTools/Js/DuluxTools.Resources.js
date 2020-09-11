(function () {
    'use strict';
    angular.module('umbraco.resources').factory('duluxToolsResources', function ($q, $http, umbRequestHelper, duluxToolsConfig) {
        return {
          downloadSiteContent: function () {
            var url = umbRequestHelper.convertVirtualToAbsolutePath(duluxToolsConfig.baseApiUrl + "/DuluxExportApi/DownloadSiteContent");
            return umbRequestHelper.downloadFile(url);
          },
          importProducts: function (data) {
            var url = umbRequestHelper.convertVirtualToAbsolutePath(duluxToolsConfig.baseApiUrl + "/DuluxImportApi/ImportProducts");
            return umbRequestHelper.postMultiPartRequest(url, data);
          },
          importSeedProducts: function (data) {
            var url = umbRequestHelper.convertVirtualToAbsolutePath(duluxToolsConfig.baseApiUrl + "/DuluxImportApi/ImportSeedProducts");
            return umbRequestHelper.postMultiPartRequest(url, data);
          }
        };
    });
})();