'use strict';

app.controller('reportBugController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {
        
        $scope.savedSuccessfully = false;
        $scope.message = "";
        $scope.bugerror = {
            bugError: ""
        }

        $scope.authLog = function (isLoggedIn) {
            var isLoggedIn = authService.authentication.isAuth;
            if (isLoggedIn)
                return true;
            else
                return false;
        };


        $scope.savebugError = function () {
            $scope.bugerror.userName = authService.authentication.userName;
            infraDataService.savebugError($scope.bugerror)
                .then(function (response) {
                    $scope.savedSuccessfully = true;
                    $scope.message = "Your error message has been sent successfully!";
                    $scope.bugerror.bugError = "";
                    
                },
                function (response) {

                });
        };
    }
]);