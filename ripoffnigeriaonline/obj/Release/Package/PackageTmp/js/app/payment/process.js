'use strict';

/* Controllers */

// Form controller
app.controller('processPaymentCtrl', ['$scope', '$state', '$stateParams', 'infraDataService', 'firmPreference', 'authService', '$timeout', function ($scope, $state, $stateParams, infraDataService, firmPreference, authService,$timeout) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.isSaving = false;

    $scope.bankId = 0;

    $scope.payment = {
        clientMeetingRequestId: 0,
        bankId: 0,
        accountName: "",
        accountNumber: 0
    }

    $scope.myPromise = infraDataService.getBanks()
    .then(function (result) {
        $scope.banks = result;
    });


    $scope.processPayment = function () {
        $scope.isSaving = true;
        $scope.payment.clientMeetingRequestId = $stateParams.clientId;
        $scope.payment.bankId = $scope.bankId;
        infraDataService.saveTransactionDetails($scope.payment)
            .then(function (response) {
                $scope.savedSuccessfully = true;
                $scope.message = "Your request has been sent successfully!";
                $state.go('app.success')
            },
            function (responses) {
                if (responses == 400) {
                    $scope.message = "Your request was not successful!";
                    $scope.isSaving = false;
                    startTimerResetPage();

                }
                else {
                    $scope.message = "Your session has expired,please Login again.";
                    $state.go('access.signin');
                }
            });
    }

    var startTimerResetPage = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            window.location.reload();
        }, 3000);
    };


}])
;


