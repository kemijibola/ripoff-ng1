﻿'use strict';

/* Controllers */

// Form controller
app.controller('processPaymentCtrl', ['$scope', '$state', '$stateParams', 'infraDataService', 'firmPreference', 'authService', function ($scope, $state, $stateParams, infraDataService, firmPreference, authService) {

    $scope.userId = infraDataService.useridF;

}])
;
