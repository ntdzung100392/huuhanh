(function () {
  'use strict';
  angular.module("umbraco").controller("DuluxTools.Import.Controller",
    function ($routeParams, navigationService, duluxToolsConfig, duluxToolsResources, notificationsService) {
      var vm = this;
      vm.config = duluxToolsConfig.config;

      navigationService.syncTree({ tree: $routeParams.tree, path: [-1, $routeParams.method], forceReload: false });

      vm.imports = [
        { value: "importProducts", label: "Import products" },
        { value: "importSeedProducts", label: "Import seed products" }
      ];

      vm.selectImportType = function () {
        console.log('import type selected');
      };

      vm.importFile = function () {
        console.log('import type ' + vm.selectedImportType);

        switch (vm.selectedImportType) {
          case "importProducts":
            vm.isLoading = true;

            var file = document.getElementById('fileToImport').files[0],
                fileReader = new FileReader();

            fileReader.onloadend = function(e) {
              const data = [
                {
                  key: 'importFromRow',
                  value: vm.importFromRow
                },
                {
                  key: 'importFile',
                  value: e.target.result
                }
              ];

              duluxToolsResources.importProducts(data).then(function (response) {
                vm.isLoading = false;
                var importResult = response.data;
                if (importResult.isValid) {
                  notificationsService.success('Import', 'import product successfully');
                } else {
                  notificationsService.error('Import', 'import product failed');
                }

                console.log(importResult);
              }, function (response) {
                notificationsService.error('Import', 'import product failed');
              });
            };

            fileReader.readAsDataURL(file);

            break;

          case "importSeedProducts":
            vm.isLoading = true;

            var file = document.getElementById('fileToImport').files[0],
                fileReader = new FileReader();

            fileReader.onloadend = function (e) {
              const data = [
                {
                  key: 'importFromRow',
                  value: vm.importFromRow
                },
                {
                  key: 'importFile',
                  value: e.target.result
                }
              ];

              duluxToolsResources.importSeedProducts(data).then(function (response) {
                vm.isLoading = false;
                var importResult = response.data;
                if (importResult.isValid) {
                  notificationsService.success('Import', 'import product successfully');
                } else {
                  notificationsService.error('Import', 'import product failed');
                }

                console.log(importResult);
              }, function (response) {
                notificationsService.error('Import', 'import product failed');
              });
            };

            fileReader.readAsDataURL(file);

            break;
          default:
            break;
        }
      };
    });
})();