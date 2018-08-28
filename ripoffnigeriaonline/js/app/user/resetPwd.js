'use strict';

app.controller('resetPwdController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.isSaving = false;
        $scope.message = "";
        $scope.savedSuccessfully = false;

        $scope.userPassword = {
            userId: "",
            token: "",
            NewPassword: "",
            ConfirmPassword: "",
        };

        $scope.resetPassword = function () {
            $scope.isSaving = true;
            $scope.userPassword.userId = $stateParams.id;
            $scope.userPassword.token = $stateParams.token;
            infraDataService.resetPassword($scope.userPassword)
                .then(function (response) {

                    // console.log('signup success: ' + response);
                    $scope.savedSuccessfully = true;
                    $scope.message = "Your password has been reset successfully!";
                    startTimer();

                },

                function (err) {
                    $scope.message = "Token expired/invalid. Request for new password reset"
                }

                );
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                state.go('access.signin');
            }, 2000);
        };

    }
]);