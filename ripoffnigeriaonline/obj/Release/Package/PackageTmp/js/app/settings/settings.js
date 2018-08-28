app.controller('UserSettingsCtrl', ['$scope', '$http', 'infraDataService', 'authService', '$state','$timeout', function ($scope, $http, infraDataService, authService,$state,$timeout) {

    $scope.username = "";
    $scope.message = "";
    $scope.isSaving = false;
    $scope.savedSuccessfully = false;
    $scope.userSettings = {};
    $scope.reportSettings = {};
    $scope.interestedLawyer = false;
    $scope.allAdvocate = false;
    $scope.commentonmyReport = false;
    $scope.commentonmyRebuttal = false;
    $scope.username = authService.authentication.userName;


    $scope.myPromise = $http.get('api/accounts/user/' + $scope.username)
    .then(function (result) {
       $scope.userSettings = result.data;
   
    });


    $scope.updateUserAccount = function () {
        $scope.isSaving = true;
        infraDataService.updateUserAccount($scope.userSettings)
            .then(function (response) {
                $scope.savedSuccessfully = true;
                $scope.message = "Your settings has been updated successfully.";
                $scope.isSaving = false;
                $scope.userSettings;
            },
            function (response) {
                $scope.message = "Failed to update settings due to.";
            });
    };


}]);