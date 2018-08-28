app.controller('approvalReportCtrl', ['$scope', 'infraDataService', function ($scope, infraDataService) {


    $scope.getUnapprovedReport = [];


    $scope.currentPage = 1;
    $scope.pageSize = 25;

    $scope.pageChangeHandler = function (num) {


    };

    $scope.myPromise = infraDataService.GetReporByStatusApproved()
    .then(function (result) {
        $scope.getUnapprovedReport = result;
        $scope.allreportLength = result.length;
    });


}])
;