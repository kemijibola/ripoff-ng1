'use strict';

app.controller('contactController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', '$stateParams', function ($scope, http, infraDataService, $location, state, $timeout, authService, $stateParams) {

        $scope.savedSuccessfully = false;
        $scope.message = "";
        $scope.feedback = {
            FeedbackMssg: ""
        }

        $scope.authLog = function (isLoggedIn) {
            var isLoggedIn = authService.authentication.isAuth;
            if (isLoggedIn)
                return true;
            else
                return false;
        };


        $scope.saveFeedback = function () {
            $scope.feedback.userName = authService.authentication.userName;
            infraDataService.saveFeedback($scope.feedback)
                .then(function (response) {
                    $scope.savedSuccessfully = true;
                    $scope.message = "Your message has been sent successfully!";
                    $scope.feedback.FeedbackMssg = "";
                },
                function (response) {

                });
        };
    }
]);