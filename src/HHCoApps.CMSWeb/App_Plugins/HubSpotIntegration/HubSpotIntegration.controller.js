var app = angular.module("umbraco");
// APIs
const oauthAPI = (clientId, redirectUri, scopes) => (`https://app.hubspot.com/oauth/authorize?client_id=${encodeURIComponent(clientId)}&redirect_uri=${encodeURIComponent(redirectUri)}&scope=${encodeURIComponent(scopes)}`);

app.service('hubSpotOAuthService', function () {
  this.authentication = function (clientId, redirectUri, scopes) {
    const scopesSeperated = scopes.replace(",", " ");
    window.location.href = oauthAPI(clientId, redirectUri, scopesSeperated);
  }
});

app.service('hubSpotBackOfficeService', function ($http) {
  this.getHubSpotCredentials = function () {
    const url = window.location.origin + "/umbraco/BackOffice/Api/RefreshTokenHubSpot/GetHubSpotCredentials";
    return $http.get(url).then(function (result) {
      return result.status === 200 ? result.data : [];
    }, function (err) {
      console.log(err);
    })
  };
});

function HubspotRefreshTokenController($scope, hubSpotOAuthService, hubSpotBackOfficeService) {
  $scope.showModal = false;
  // hubspot credentials
  $scope.clientId = "";
  $scope.redirectUri = "";
  $scope.scopeName = "";

  setTimeout(async function () {
    const configs = await hubSpotBackOfficeService.getHubSpotCredentials();
    $scope.clientId = configs.clientId;
    $scope.redirectUri = configs.redirectUri;
    $scope.scopeName = configs.scopes.join(",");
  }, 100);

  // methods
  $scope.toggleModal = function () {
    $scope.showModal = !$scope.showModal;
  };

  $scope.redirectToHubSpot = function () {
    if ($scope.clientId != "" && $scope.redirectUri != "" && $scope.scopeName != "") {
      hubSpotOAuthService.authentication($scope.clientId, $scope.redirectUri, $scope.scopeName);
    }
  }
}

app.controller("HubspotRefreshTokenController", HubspotRefreshTokenController);