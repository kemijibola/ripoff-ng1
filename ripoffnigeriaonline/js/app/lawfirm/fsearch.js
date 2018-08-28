'use strict';

/* Controllers */

// Form controller
app.controller('fsearch', ['$scope', '$state', '$stateParams', 'infraDataService', 'firmPreference', function ($scope, $state, $stateParams, infraDataService, firmPreference) {

    $scope.currentPage = 1;
    $scope.pageSize = 1;
    $scope.lawbyfirmcategories = [];


    $scope.pageChangeHandler = function (num) {
    };

    $scope.onSelected = function (item) {
        $scope.lawCategoryId = item.id;
    };

    infraDataService.getLawCategory()
    .then(function (result) {
        $scope.lawyerPreference = result;
    });

    $scope.searchFirmByName = function () {
        firmPreference.categoryId = $scope.lawCategoryId;
        infraDataService.getLawfirmByFirmPreference($scope.lawCategoryId)
            .then(function (result) {
                $scope.lawbyfirmcategories = result;
            });
    };

    $scope.searchAllPreference= function () {
        infraDataService.getALLLawfirm()
            .then(function (result) {
                $scope.lawbyfirmcategories = result;
               // console.log($scope.lawbyfirmcategories[0].ripFirm[0].firmImages[0].imagePath);
            });
    };

}])
;


