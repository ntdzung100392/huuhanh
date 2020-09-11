(function () {
  'use strict';
  angular.module("umbraco").controller("DuluxTools.Export.Controller",
    function ($window, $routeParams, navigationService, duluxToolsConfig, duluxToolsResources) {
      var vm = this;
      vm.config = duluxToolsConfig.config;

      navigationService.syncTree({ tree: $routeParams.tree, path: [-1, $routeParams.method], forceReload: false });

      vm.exports = [
        { value: "contentSiteMap", label: "Content Site Map" }
      ];

      vm.selectExportForDownload = function () {
        console.log('export selected');
      };

      vm.exportFile = function () {
        console.log('export file ' + vm.selectedExport);

        switch (vm.selectedExport) {
          case "contentSiteMap":
            vm.isLoading = true;
            duluxToolsResources.downloadSiteContent().then(function() {
              vm.isLoading = false;
            });

            break;
          default:
            break;
        }
      };
    });
})();