app.controller('approveReportCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$modal', '$log', function ($scope, $http, $stateParams, infraDataService, authService, $modal, $log) {

    $scope.reportDetails = [];
    $scope.rejectionReasons = [];
    
    $scope.report = {
        status: false,
        rejectionId: 0
    }
    $scope.reportStatus = false;
    $scope.onRoleChecked = function () {
        if ($scope.report.status) {
            $scope.reportStatus = true;
            //console.log($scope.reportStatus);

        } else {
            $scope.reportStatus = false;
            //console.log($scope.reportStatus);
        }

    };
    console.log($scope.report.rejectionId);
    infraDataService.getRejectionReason()
    .then(function (res) {
        $scope.rejectionReasons = res;
    });

    $scope.myPromise = infraDataService.getReportDetailById($stateParams.reportId)
        .then(function (result) {
            $scope.reportDetails = result;
        });

    $scope.update = function () {
        $scope.updateUserReport = {};
        $scope.updateUserReport.reportId = $stateParams.reportId,
        $scope.updateUserReport.reportStatus = $scope.reportStatus;
        console.log($scope.updateUserReport);
        infraDataService.updateReport($scope.updateUserReport)
            .then(function (response) {
              $scope.savedSuccessfully = true;
                $scope.message = "Report status updates successfully!";

            },
            function (response) {
            });
    };

}]);