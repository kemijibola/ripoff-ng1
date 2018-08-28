app.controller('ThankYouCtrl', ['$scope', 'infraDataService', function ($scope, infraDataService) {


    $scope.getallreportByDates = [];


    $scope.myPromise = infraDataService.getAllReportByDate()
    .then(function (result) {
        $scope.getallreportByDates = result;
        $scope.allreportLength = result.length;
    });


}])
;