app.directive('match', function ($parse) {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            scope.$watch(function () {
                return $parse(attrs.match)(scope) === ctrl.$modelValue;
            }, function (currentValue) {
                ctrl.$setValidity('mismatch', currentValue);
            });
        }
    };
});
app.directive('uniqueUsername', function (isUsernameAvailable) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.unique = isUsernameAvailable;
        }
    };
});
app.directive('uniqueEmail', function (isEmailAvailable) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.unique = isEmailAvailable;
        }
    };
});

app.directive('uniqueReport', function (isReportAvailable) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.unique = isReportAvailable;
        }
    };
});

app.factory('isUsernameAvailable', function ($q, $http) {
    return function (username) {
        var deferred = $q.defer();

        $http.get('api/accounts/GetRegisteredUsername/' + username).then(function () {
            // Found the user, therefore not unique.
            deferred.reject();
        }, function () {
            // User not found, therefore unique!
            deferred.resolve();
        });

        return deferred.promise;
    }
});
app.factory('isEmailAvailable', function ($q, $http) {
    return function (email) {
        var deferred = $q.defer();

        $http.get('api/accounts/GetRegisteredEmail/' + email).then(function () {
            // Found the user, therefore not unique.
            deferred.reject();
        }, function () {
            // User not found, therefore unique!
            deferred.resolve();
        });

        return deferred.promise;
    }
});
app.factory('isReportAvailable', function ($q, $http) {
    return function (reportId) {
        var deferred = $q.defer();

        $http.get('api/report/' + reportId).then(function (result) {
            if (result.data.length > 0) {
                deferred.resolve();
               
            }
            else {
                deferred.reject();
            }

        });
        return deferred.promise;
        }
});


app.directive('passwordValidate', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {

                scope.pwdValidLength = (viewValue && viewValue.length >= 8 ? 'valid' : undefined);
                scope.pwdHasLowerCase = (viewValue && /[a-z]/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasCapital = (viewValue && /[A-Z]/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasNumber = (viewValue && /\d/.test(viewValue)) ? 'valid' : undefined;
                scope.pwdHasSymbol = (viewValue && /[-!$%^&*()_+@|~=`{}\[\]:";'<>?,.\/]/.test(viewValue)) ? 'valid' : undefined;

                if (scope.pwdValidLength && scope.pwdHasLowerCase && scope.pwdHasNumber && scope.pwdHasCapital && scope.pwdHasSymbol) {
                    ctrl.$setValidity('pwd', true);
                    return viewValue;
                } else {
                    ctrl.$setValidity('pwd', false);
                    return undefined;
                }

            });
        }
    };
});


app.directive('usernameAvailableValidator', function ($http, $timeout, $q) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.usernameAvailable = function (username) {
                return $http.get('api/login/GetRegisteredUsername/' + username)
                 .then(function (result) {
                     scope.checkUsers = result.data;
                     var deferred = $q.defer();
                     $timeout(function () {
                         if (scope.checkUsers['status'] == 404) {
                             deferred.reject();
                         } else {
                             deferred.resolve();
                         }
                     }, 2000);
                     return deferred.promise;
                 });

                return defer.promise;
            }
        }
    };
});


app.directive('userExistsValidator', function ($q, User) {
    return {
        require: 'ngModel',
        link: function ($scope, element, attrs, ngModel) {
            ngModel.$asyncValidators.exists = function (value) {
                var defer = $q.defer();
                var params = {};
                params[attrs.name] = value;

                User.exists(params, function (data) {
                    if (data.exists) {
                        defer.reject();
                    } else {
                        defer.resolve();
                    }
                });
                return defer.promise;
            };
        }
    };
});

