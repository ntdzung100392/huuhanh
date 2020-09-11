(function () {
    'use strict';
    angular.module("umbraco")
        .constant("duluxToolsConfig", {
            "baseApiUrl": "~/umbraco/BackOffice/Api/",
            "config": {
                "version": "1.0.0",
                "baseTreeUrl": "#/duluxTools/",
                "baseViewUrl": "#/duluxTools/"
            }
        });
})();