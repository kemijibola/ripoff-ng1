'use strict';

app.controller('RoleController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.savedSuccessfully = false;
        $scope.message = "";
        $scope.name = "";


        $scope.saveHubRole = function () {
            console.log($scope.name);
            infraDataService.saveHubRole($scope.name)
                .then(function (response) {
                    $scope.savedSuccessfully = true;
                    $scope.message = "Role has been added successfully!";
                    $scope.name = "";
                },
                function (response) {
                    $scope.message = "Role already exist!";
                    $scope.name = "";
                });
        };
    }
]);