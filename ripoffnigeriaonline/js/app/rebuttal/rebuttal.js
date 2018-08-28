'use strict';

/* Controllers */

// Form controller
app.controller('rebuttalFormCtrl', ['$scope', '$http', 'infraDataService', '$stateParams', 'ripoffConstants', 'wizardDataService', 'FileUploader', '$timeout','$state', function ($scope, $http, infraDataService, $stateParams, ripoffConstants, wizardDataService, FileUploader,$timeout,state) {


    var reportId = $stateParams.reportId;
    var userId = infraDataService.useridF;
    $scope.states = [];
    $scope.cities = [];
    $scope.reportDetails = [];
    $scope.isSaving = false;

    $scope.newRebuttal = {
        StateId: 0,
        reportId: 0,
        userId: ""
    };

    infraDataService.getState()
    .then(function (result) {
        angular.copy(result, $scope.states);

    });

    $scope.myPromise = infraDataService.getReportDetailById($stateParams.reportId)
    .then(function (result) {
        $scope.reportDetails = result;
    });

    $scope.save = function () {
        $scope.isSaving = true;
        $scope.newRebuttal.userId = userId;
        $scope.newRebuttal.reportId = reportId;
        infraDataService.saveRebuttal($scope.newRebuttal)
            .then(function (response) {
                startTimer();
            },
            function (response) {      
            });
    };
    $scope.getCityByState = function () {
        infraDataService.getCityByState($scope.newRebuttal.StateId).
            then(function (data) {
                $scope.cities = data;
            });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            state.go('app.form.usuccess');
        }, 2000);
    };

}])
;