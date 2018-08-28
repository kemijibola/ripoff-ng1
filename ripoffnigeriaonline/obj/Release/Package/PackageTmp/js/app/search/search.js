app.controller('SearchCtr', ['$scope', '$location', 'infraDataService', '$stateParams', '$state', 'localStorageService', 'searchService', function ($scope, $location, infraDataService, $stateParams, $state, localStorageService, searchService) {



    $scope.searchCompanyByName = function () {
        
        $scope.searchCompany = "";
        $scope.resSearch = [];
        searchService.companyName = $stateParams.param;
        $scope.companySearchingFor = searchService.companyName
        if (searchService.companyName) {
            $scope.myPromise = searchService.searchCompany(searchService.companyName)
             .then(function (response) {
                 $scope.resSearch = response.data;
                 $scope.searchLength = response.data.length;
             },
                function (response) {
                    
                });
        }
    };
    $scope.searchCompanyByName();
    $scope.currentPage = 1;
    $scope.pageSize = 5;

    $scope.pageChangeHandler = function (num) {


    };


    // Any function returning a promise object can be used to load values asynchronously
    $scope.getLocation = function (val) {
        return $http.get('http://maps.googleapis.com/maps/api/geocode/json', {
            params: {
                address: val,
                sensor: false
            }
        }).then(function (res) {
            var addresses = [];
            angular.forEach(res.data.results, function (item) {
                addresses.push(item.formatted_address);
            });
            return addresses;
        });
    };
}])
;