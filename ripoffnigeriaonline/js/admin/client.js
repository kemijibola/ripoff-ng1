'use strict';

app.controller('ClientController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.client = {
            id: "",
            secret: "",
            Name:"",
            allowedOrigin: ""
        }

        $scope.save = function () {
            //console.log($scope.name);
            infraDataService.saveClient($scope.client)
                .then(function (response) {
                    $scope.savedSuccessfully = true;
                    $scope.message = "Client has been added successfully!";

                },
                function (response) {
                });
        };
    }
]);