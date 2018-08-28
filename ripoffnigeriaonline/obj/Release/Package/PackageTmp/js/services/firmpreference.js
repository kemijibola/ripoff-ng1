'use strict';

app.factory('firmPreference', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://rip-offnigeria.com/';
    var firmServiceFactory = {};

    var _categpryId = 0;

    var arrSearchResults = [];

    var _searchFirmByCategory = function (categoryId) {
        if (categoryId) {
            var deferred = $q.defer();
            $http.get(serviceBase + 'api/lawcategory/' + categoryId)
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
    firmServiceFactory.searchFirmByCategory = _searchFirmByCategory;
    firmServiceFactory.categpryId = _categpryId;

    return firmServiceFactory;
}]);