'use strict';

app.controller('forgotPwdController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.registeredEmail = {
            email: ""
        }
        $scope.email = "";
        $scope.message = "";
        $scope.isSaving = false;
        $scope.sendResetLink = function () {
            $scope.isSaving = true;
            infraDataService.forgotPassword($scope.registeredEmail.email)
                .then(function (response) {
                    state.go('app.reset');
                    $scope.result = response;
                    $scope.message = "A link to reset your password has been sent to your email";
    

                },
                function (response) {
                    console.log('signup error: ' + response)
                    var errors = [];
                    for (var key in response.data.modelState) {
                        for (var i = 0; i < response.data.modelState[key].length; i++) {
                            errors.push(response.data.modelState[key][i]);
                        }
                    }
                    $scope.message = "Failed to send link to reset your password:" + errors.join(' ');
                });
        };


    }
]);