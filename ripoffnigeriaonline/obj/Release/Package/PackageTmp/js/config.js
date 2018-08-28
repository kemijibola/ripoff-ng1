// config

var app =  
angular.module('app')
  .config(
    [        '$controllerProvider', '$compileProvider', '$filterProvider', '$provide',
    function ($controllerProvider,   $compileProvider,   $filterProvider,   $provide) {
        
        // lazy controller, directive and service
        app.controller = $controllerProvider.register;
        app.directive  = $compileProvider.directive;
        app.filter     = $filterProvider.register;
        app.factory    = $provide.factory;
        app.service    = $provide.service;
        app.constant   = $provide.constant;
        app.value      = $provide.value;
    }
  ])
  .config(['$translateProvider', function($translateProvider){
    // Register a loader for the static files
    // So, the module will search missing translation tables under the specified urls.
    // Those urls are [prefix][langKey][suffix].
    $translateProvider.useStaticFilesLoader({
      prefix: 'l10n/',
      suffix: '.js'
    });
    // Tell the module what language to use by default
    $translateProvider.preferredLanguage('en');
    // Tell the module to store the language in the local storage
    $translateProvider.useLocalStorage();
  }]);

var serviceBase = 'http://rip-offnigeria.com/';
//var serviceBase = 'http://localhost/ripoffnigeriaonline/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: '476965559161744'
});

app.run(['authService', function (authService) {
    authService.fillAuthData();

}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(function (editableOptions) {
    editableOptions.theme = 'bs3';
});

app.run(['$rootScope', '$location', '$state', 'authService', 'uuid2', function ($rootScope, $location, $state, authService, uuid2) {
    $rootScope.$on('$stateChangeStart', function (ev, to, toParams, from, fromParams) {

        var authReq = ['app.settings', 'app.profile', 'app.form', 'app.form.report', 'app.form.rebuttal', 'app.form.update','admin.register', 'admin.roles', 'admin.client', 'admin.dashboard', 'admin', 'admin.assign'];
        if (!authService.authentication.isAuth) {

            for (var i = 0; i < authReq.length; i++) {
                authReqIndex = authReq[i];
                if (to.name == authReqIndex) {
                    ev.preventDefault();
                    authService.logOut();
                    $rootScope.returnToState = to.url;
                    $rootScope.returnToStateParams = toParams.Id;
                    $rootScope.reportId = fromParams.reportId;
                    $state.go('access.signin');   
                }
                else {

                }
            }
        }
        else { }


    }),


    $rootScope.$on('$stateChangeStart',
    function (event, toState, toParams, fromState, fromParams) {
        var append = { append: uuid2.newguid() };
        toParams.append = append.append;
        // do something
    })


}]);