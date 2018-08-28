app.controller('UserProfileCtrl', ['$scope', '$http', 'infraDataService', 'authService', function ($scope, $http, infraDataService, authService) {

    $scope.username = "";
    $scope.user = {};
    $scope.countries = [];
    $scope.userState = [];
    $scope.userCity = [];
    $scope.states = [];
    $scope.cities = [];
    $scope.countryId = 0;
    $scope.stateId = 0;
    $scope.cityId = 0;
    $scope.message = "";
    $scope.editorEnabled = false;
    $scope.savedSuccessfully = false;
    
    $scope.username = authService.authentication.userName;

    $scope.enableEditor = function () {
        $scope.editorEnabled = true;
        //$scope.editableTitle = $scope.title;
    };

    $scope.disableEditor = function () {
        $scope.editorEnabled = false;
    };

    $scope.myPromise = $http.get('api/accounts/user/' + $scope.username)
    .then(function (result) {
        $scope.user = result.data;
        $scope.user.selected = true;
   });

    infraDataService.getCountry().then(function (data) {
        $scope.countries = data;
    });

    infraDataService.getState().then(function (result) {
        $scope.userState = result;
    });

    infraDataService.getCity().then(function (resp) {
        $scope.userCity = resp;
    });

    $scope.updateUserAccount = function () {
        infraDataService.updateUserAccount($scope.user)
            .then(function (response) {
                $scope.savedSuccessfully = true;
                $scope.message = "Your account has been updated successfully.";
            },
            function (response) {
                $scope.message = "Failed to update user due to.";
            });
    };

}]);