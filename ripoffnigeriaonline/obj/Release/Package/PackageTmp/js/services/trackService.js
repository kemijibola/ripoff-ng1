'use strict';
app.factory('trackDataService', ['$http', '$q', 'authService', function ($http, $q, authService) {

    
    var serviceBase = 'http://rip-offnigeria.com/';
    
    var trackServiceFactory = {};

    var username = authService.authentication.userName;
    var _hasLiked = false;


  
    trackServiceFactory.hasLiked = _hasLiked;
    
   

    return trackServiceFactory;
}]);