'use strict';

app.factory('searchService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://rip-offnigeria.com/';
    var searchServiceFactory = {};
    
    var _companyName = "";

    var arrSearchResults = [];

    var _searchCompany = function (companyName) {
        if (companyName) {
            var deferred = $q.defer();
            $http.get(serviceBase + 'api/report/SearchByCompanyName/' + companyName)
               .then(function (response) {
                   deferred.resolve(response);
               }, function (fault) {
                   deferred.reject();
               });
            return deferred.promise;
        }
        else {
            arrSearchResults = [];
        }
   
    };
    searchServiceFactory.searchCompany = _searchCompany;
    searchServiceFactory.companyName = _companyName

        return searchServiceFactory;
}]);