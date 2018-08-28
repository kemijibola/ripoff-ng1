app.controller('ReportDetailCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$modal', '$log', function ($scope, $http, $stateParams, infraDataService, authService, $modal, $log) {

    $scope.reportDetails = [];
    $scope.rebuttalUserName = "";
    $scope.reportLength = 0;
    $scope.rebuttalLength = 0;
    $scope.isreportGreaterThanOne = false;
    $scope.isrebuttalGreaterThanOne = false;


    $scope.myPromise = infraDataService.getReportDetailById($stateParams.reportId)
        .then(function (result) {
            $scope.reportDetails = result;
            try
            {
                $scope.reportLength = $scope.reportDetails[0].report.rebuttals.length;
                $scope.rebuttalLength = $scope.reportDetails[0].report.reportImages.length;
                if ($scope.reportLength > 1 || $scope.rebuttalLength > 1) {
                    $scope.isreportGreaterThanOne = true;
                }
                else {
                    $scope.isreportGreaterThanOne = false;
                }
                $scope.userId = $scope.reportDetails[0].report.rebuttals[0].userId;

                if($scope.userId != "")
                    infraDataService.GetUserNameById($scope.userId)
                    .then(function (data) {
                        $scope.rebuttalUserName = data;
                    }),
                    console.log('abc');
            }
            catch(e)
            {

            }
        });

}]);