
'use strict';


app.controller('SigninFormController', ['$scope', '$location', 'authService', 'infraDataService', '$rootScope', '$state', 'ngAuthSettings', function ($scope, $location, authService, infraDataService, $rootScope, $state, ngAuthSettings) {

    $scope.loginData = {
        userName: "",
        password: "",
        userid: 0

    };

    $scope.message = "";
    $scope.user = "";
    $scope.isLogging = false;


    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {
            $scope.isLogging = true;
           infraDataService.GetUserIdByUsernamePassword($scope.loginData.userName, $scope.loginData.password)
              .then(function (data)
              {
                  infraDataService.useridF = data;
                  infraDataService.GetTypeOfUserById($scope.loginData.userName)
                    .then(function (result)
                    {
                        $scope.user = result;
                        if ($scope.user === "GeneralUser")
                        {                            
                            if ($rootScope.returnToState === "/report") {
                                $state.go('app.form.report');
                                $rootScope.returnToState = "";

                            }
                            else if ($rootScope.returnToState === "/myReports") {
                                $state.go('app.form.update');
                                $rootScope.returnToState = "";
                            }
                            else if ($rootScope.returnToState === "/rebuttal/{reportId}/{append}") {

                                $rootScope.$on('$stateChangeStart',
                                    function (event, toState, toParams, fromState, fromParams) {
                                        if (toState.name === 'app.form.rebuttal') {
                                            toParams.reportId = $rootScope.reportId;
                                        }
                                    })
                                $state.go('app.form.rebuttal');
                                $rootScope.returnToState = "";
                            }
                            else {
                                $location.path('app.ripoff');
                            }
                        }
                        else if ($scope.user !== "GeneralUser")
                        {
                            authService.logOut();
                            $scope.message = "You don't have access to this page";
                            
                        }
                    })   
            });
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };


    $scope.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/accounts/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    $scope.authCompletedCB = function (fragment) {

        $scope.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('app.associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                    $location.path('app.ripoff');

                },
             function (err) {
                 $scope.message = err.error_description;
             });
            }

        });
    }




}]);