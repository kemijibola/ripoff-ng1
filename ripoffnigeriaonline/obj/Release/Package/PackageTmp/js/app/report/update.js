app.controller('updateReportCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$state', function ($scope, $http, $stateParams, infraDataService, authService, $state, localStorageService) {


    $scope.userName = authService.authentication.userName;
    $scope.userReports = [];
    $scope.Approved = false;
    $scope.clientMeeting = [];


    $scope.config = {
        itemsPerPage: 5,
        fillLastPage: true
    }
    $scope.clearFilter = function () {
        $('.filter-status').val('');
        $('.footable').trigger('footable_clear_filter');
    };
    $scope.filteringEventHandler = function (e) {
        var selected = $('.filter-status').find(':selected').text();
        if (selected && selected.length > 0) {
            e.filter += (e.filter && e.filter.length > 0) ? ' ' + selected : selected;
            e.clear = !e.filter;
        }
    };
    $scope.filterByStatus = function () {
        $('.footable').trigger('footable_filter', {
            filter: $('#filter').val()
        });
    };

    $scope.filter = {
        status: null
    };

    $scope.reloadRoute = function () {
        $state.reload();
    };
    console.log($scope.userName);
    $scope.myPromise = infraDataService.GetUserIdByUsername($scope.userName)
        .then(function (data) {
            $scope.userId = data;
            infraDataService.getReportByUserId($scope.userId)
            .then(function (result) {
                $scope.userReports = result;
                console.log($scope.userReports);
                
            })
        })


}]);