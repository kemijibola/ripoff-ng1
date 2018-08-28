'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {


   //var serviceBase = 'http://localhost/ripoffnigeriaonline/';
  var serviceBase = 'http://rip-offnigeria.com/';

    
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
        
    };
    var _externalAuthData = {
        provider: "",
        userName: ""
    };
    var _saveRegistration = function (registration) {

        return $http.post(serviceBase + 'api/login/register', registration, {}).
           then(function (response) {
               return response;

           }, function (error) {
               return error;
           });
    };

    var getRegisteredUsers = function () {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/login/getusers/' + userid)
            .then(function (response) {
                deferred.resolve(response);
            }, function (fault) {
                deferred.reject();
            });
        return deferred.promise;
    };

    var getUsers = function (userid) {
        var deferred = $q.defer();
        $http.get(serviceBase + 'api/login/getusers/' +userid)
            .then(function (response) {
                deferred.resolve(response);
            }, function (fault) {
                deferred.reject();
            });
        return deferred.promise;
    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName});

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };


    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }

    }

    var _checkUniqueValue = function (id, property, value) {

        var data = {
            id: id,
            property: property,
            value: value
        };
        return $http.get('api/login/GetRegisteredUsername/' + data)
            .then(function (res) {
            return res.data.status;
        });
    }

    var _obtainAccessToken = function (externalData) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/accounts/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

            _authentication.isAuth = true;
            _authentication.userName = response.userName;
            _authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.checkUniqueValue = _checkUniqueValue;

    authServiceFactory.externalAuthData = _externalAuthData;
    authServiceFactory.obtainAccessToken = _obtainAccessToken;

    return authServiceFactory;
}]);