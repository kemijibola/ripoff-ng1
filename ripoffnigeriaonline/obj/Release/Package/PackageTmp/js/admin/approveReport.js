app.controller('approveReportCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$modal', '$log', function ($scope, $http, $stateParams, infraDataService, authService, $modal, $log) {

    $scope.reportDetails = [];
    $scope.radio = {
        option: false
    }

    $scope.myPromise = infraDataService.getReportDetailById($stateParams.reportId)
        .then(function (result) {
            $scope.reportDetails = result;
        });

    console.log($scope.radio.option);
}]);