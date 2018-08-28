app.controller('lawSuitCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$modal', '$log', function ($scope, $http, $stateParams, infraDataService, authService, $modal, $log) {

    $scope.reportId = $stateParams.reportId;
    $scope.caseClosed = false;

    $scope.myPromise = infraDataService.getReportDetailById($stateParams.reportId)
        .then(function (result) {
            $scope.reportDetails = result;
            var arrayCount = $scope.reportDetails[0].report.caseUpdates.length;
            var arraySize = arrayCount - 1;
            $scope.status = $scope.reportDetails[0].report.caseUpdates[arraySize].status;
            
            if($scope.status == "Case Closed")
            {
                $scope.caseClosed = true;
            }
            else {
                $scope.caseClosed = false;
            }

            $scope.firmId = $scope.reportDetails[0].report.caseUpdates[0].lawfirmId;
            infraDataService.getLawFirmById($scope.firmId)
            .then(function (data) {
                $scope.firmInCharge = data;
            })
        });





}]);