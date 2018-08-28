app.controller('latestReportCtrl', ['$scope', 'infraDataService', function ($scope, infraDataService) {


    $scope.getallreportByDates = [];


    $scope.currentPage = 1;
    $scope.pageSize = 25;

    $scope.pageChangeHandler = function (num) {


    };

    $scope.myPromise = infraDataService.getAllReportByDate()
    .then(function (result) {
        $scope.getallreportByDates = result;
        $scope.allreportLength = result.length;
    });

    //$scope.myPromise = infraDataService.getAllReportByDate()
    //.then(function (result) {
    //    $scope.getallreportByDates = result;
    //    $scope.allreportLength = result.length;
    //});

}])
;