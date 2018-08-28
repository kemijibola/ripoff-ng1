'use strict';

/* Controllers */

// Form controller
app.controller('invoiceLawyerCtrl', ['$scope', '$state', '$stateParams', 'infraDataService', 'firmPreference', 'authService', function ($scope, $state, $stateParams, infraDataService, firmPreference, authService) {

    $scope.orderNumber = $stateParams.param;

    $scope.getDateTime = new Date();

}])
;


