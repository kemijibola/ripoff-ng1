'use strict';

/* Controllers */

// Form controller
app.controller('getaLawyerCtrl', ['$scope', '$state', '$stateParams', 'infraDataService', 'firmPreference', '$timeout', 'authService', function ($scope, $state, $stateParams, infraDataService, firmPreference, authService,$timeout) {

    //$scope.savedSuccessfully = false;
    //$scope.message = "";
    $scope.isSaving = false;
    $scope.returnclientInitiationId = 0;
    $scope.firmRegionId = 0;
    $scope.alternateRegionId = 0;
    $scope.userId = infraDataService.useridF;
    $scope.reportId = $stateParams.reportId;

    
    $scope.initiateFirmMeeting = {
        userId: "",
        reportId: 0,
        firmRegionId: 0,
        alternateRegionId:0,
        email: "",
        phoneNumber: ""
    }

    infraDataService.getFirmRegion()
    .then(function (result) {
        $scope.firmregions = result;
    });


    $scope.proceedToPayment = function () {
        $scope.isSaving = true;
        $scope.initiateFirmMeeting.firmRegionId = $scope.firmRegionId;
        $scope.initiateFirmMeeting.alternateRegionId = $scope.alternateRegionId;
        $scope.initiateFirmMeeting.userId = infraDataService.useridF;
        $scope.initiateFirmMeeting.reportId = $scope.reportId;
        infraDataService.saveClientMeetingDetails($scope.initiateFirmMeeting)
            .then(function (response) {
                $scope.returnclientInitiationId = response.id;
                //$scope.savedSuccessfully = true;
                //$scope.message = "Your request has been sent successfully!";
                $state.go('app.invoice', { param: $scope.returnclientInitiationId })
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


