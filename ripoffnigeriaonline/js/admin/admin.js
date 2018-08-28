'use strict';

/* Controllers */
  // signin controller
app.controller('AdminSignInController', ['$scope', '$location', 'authService', 'infraDataService', '$rootScope', '$state', function ($scope, $location, authService, infraDataService, $rootScope, $state) {

    $scope.loginData = {
        userName: "",
        password: "",
        userid: 0

    };
    $scope.message = "";
    $scope.user = "";
    
    $scope.login = function () {

        $scope.myPromise = authService.login($scope.loginData).then(function (response) {
           infraDataService.GetUserIdByUsernamePassword($scope.loginData.userName, $scope.loginData.password)
              .then(function (data)
              {
                  infraDataService.useridF = data;
                  infraDataService.GetTypeOfUserById($scope.loginData.userName)
                    .then(function (result)
                    {
                        $scope.user = result;
                        if($scope.user === "AdminUser")
                        {
                            console.log("log");
                            $state.go('admin.dashboard');
                        }
                        else if ($scope.user !== "AdminUser")
                        {     
                            $scope.message = "You don't have access to this page";
                            authService.logOut();
                        }
                    })   
            });
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };

}]);