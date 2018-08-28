'use strict';
app.factory('ripoffService', ['$http', function ($http) {

    var serviceBase = 'http://rip-offnigeria.com/';
   // var serviceBase = 'http://localhost/ripoffnigeriaonline/';
    
    
    var ripoffServiceFactory = {};

    var _getReport = function () {

        return $http.get(serviceBase + 'api/report').then(function (results) {
            return results;
        });
    };

    ripoffService.getReport = _getReport;

    return ripoffService;

}]);

