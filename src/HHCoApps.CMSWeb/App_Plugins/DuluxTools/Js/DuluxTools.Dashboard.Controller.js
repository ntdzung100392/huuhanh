(function () {
    'use strict';
    angular.module("umbraco").controller("DuluxTools.Dashboard.Controller",
        function (duluxToolsConfig) {
            var vm = this;
            vm.config = duluxToolsConfig.config;

            vm.pages = [
                { name: "Import", url: "importExportTree/import", desc: "Import products using excel files" },
                { name: "Export", url: "importExportTree/export", desc: "Export contents to excel files" }
            ];
        });
})();