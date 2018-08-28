'use strict';

app.controller('adminNavController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.userName = authService.authentication.userName;
        $scope.isAdmin = false;
            
        infraDataService.GetTypeOfUserById($scope.userName)
        
                    .then(function (result) {
                        $scope.user = result;
                        if ($scope.user === "AdminUser") {
                            $scope.isAdmin = true;
                        }
                    })
    }
]);