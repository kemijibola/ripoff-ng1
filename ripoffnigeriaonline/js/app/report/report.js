'use strict';

/* Controllers */

// Form controller
app.controller('FormReportCtrl', ['$scope', '$http', 'infraDataService', 'ripoffConstants', 'wizardDataService', 'FileUploader', function ($scope, $http, infraDataService, ripoffConstants, wizardDataService, FileUploader) {

    $scope.states = [];
    $scope.countries = [];
    $scope.cities = [];
    $scope.topics = [];
    $scope.foragereport = [];
    $scope.countryId = 160;
    //$scope.countryId = 37;
    $scope.categories = [];
    $scope.locations = [];
    $scope.newReport = {
        stateId: 0,
        topicId:0
    };
    wizardDataService.report = $scope.newReport;

    $scope.SelectedStateChanged = function (item) {
        $scope.newReport.stateId = item.id;
    };

    $scope.getRadioButtonToCheck = function (value, property) {
        if (value === property)
            return true;
        else {
            return false;
        }
    };

    $scope.myPromise = infraDataService.getLocation()
    .then(function (result) {
        angular.copy(result, $scope.locations);
    });

    infraDataService.getStateByCountry($scope.countryId)
    .then(function (result) {
        angular.copy(result, $scope.states);

    });

    infraDataService.getTopic()
    .then(function (result) {
        angular.copy(result, $scope.topics);
    });

    $scope.getCityByState = function () {
        infraDataService.getCityByState($scope.newReport.stateId).
            then(function (data) {
                $scope.cities = data;
            });
    };

    $scope.getCategoryByTopic = function () {
        infraDataService.getCategoryByTopic($scope.newReport.topicId).
            then(function (data) {
                $scope.categories = data;
            });
    };
    $scope.save = function () {
        if (wizardDataService.report != null) {
            infraDataService.saveReport(wizardDataService.report)
                .then(function (result) {
                    //console.log(result);
                },
                    function (responses) {
                        console.log("did not submit");

                    });
        }
    };
}])
;