'use strict';

app.controller('SignupFormController', [
    '$scope', '$http', 'infraDataService', '$location', '$state', '$timeout', 'authService', 'toaster', function ($scope, http, infraDataService, $location, state, $timeout, authService, toaster) {

        $scope.savedSuccessfully = false;
        $scope.isSaving = false;
        $scope.message = "";
        $scope.countries = [];
        $scope.states = [];
        $scope.cities = [];
        $scope.countryId = 0;
        $scope.stateId = 0;
        $scope.cityId = 0;
        $scope.user = "GeneralUser";
        $scope.countryLoaded = false;
        $scope.stateLoaded = false;
        $scope.cityLoaded = false;

        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        $scope.registration = {
            userName: "",
            email: "",
            password: "",
            confirmPassword: "",
            name: "",
            address: "",
            postalCode: "",
            phoneNumber: "",
            countryId: 0,
            stateId: 0,
            cityId: 0,
            interestedLawyer: false,
            allAdvocate: false,
            commentonmyReport: false,
            commentonmyRebuttal: false,
            typeofUser: "",

        };
        $scope.toaster = {
            type: 'wait',
            title: 'Registration in progress',
            text: 'Please wait...'
        };
        $scope.signUp = function () {
            $scope.isSaving = true;
            toaster.pop($scope.toaster.type, $scope.toaster.title, $scope.toaster.text);
            $scope.registration.countryId = $scope.countryId;
            $scope.registration.stateId = $scope.stateId;
            $scope.registration.cityId = $scope.cityId;
            $scope.registration.typeofUser = $scope.user;
            infraDataService.addUserDetails($scope.registration)
                .then(function (response) {
                   // console.log('signup success: ' + response);
                    $scope.savedSuccessfully = true;
                    $scope.message = "Your account has been created successfully!";
                    startTimer();

                },
                function (responses) {
                    if(responses == 400)
                    {
                        $scope.message = "Failed to register.";
                        $scope.isSaving = false;
                        startTimerResetPage();
                        
                    }
                    else {
                        $scope.message = "Failed to register user. Please check fields.";
                        $scope.isSaving = false;
                        startTimerResetPage();

                    }
                    
                });
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                state.go('access.signin');
            }, 2000);
        };

        var startTimerResetPage = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                window.location.reload();
            }, 3000);
        };

         $scope.myPromise = infraDataService.getCountry().then(function (data) {
             $scope.countries = data;
             if(data.length >0)
             {
                 $scope.countryLoaded = true;
             }
            });
        
        $scope.getStateByCountry = function () {
            infraDataService.getStateByCountry($scope.countryId).
                then(function (data) {
                    $scope.states = data;
                    if (data.length > 0)
                    {
                        $scope.stateLoaded = true;
                    }
                    
                });
        };
        $scope.getCityByState = function () {
            infraDataService.getCityByState($scope.stateId).
                then(function (data) {
                    $scope.cities = data;
                    if (data.length > 0)
                    {
                        $scope.cityLoaded = true;
                    }
                    
                });
        };


        $scope.d = [[1, 6.5], [2, 6.5], [3, 7], [4, 8], [5, 7.5], [6, 7], [7, 6.8], [8, 7], [9, 7.2], [10, 7], [11, 6.8], [12, 7]];

        $scope.d0_1 = [[0, 7], [1, 6.5], [2, 12.5], [3, 7], [4, 9], [5, 6], [6, 11], [7, 6.5], [8, 8], [9, 7]];

        $scope.d0_2 = [[0, 4], [1, 4.5], [2, 7], [3, 4.5], [4, 3], [5, 3.5], [6, 6], [7, 3], [8, 4], [9, 3]];

        $scope.d1_1 = [[10, 120], [20, 70], [30, 70], [40, 60]];

        $scope.d1_2 = [[10, 50], [20, 60], [30, 90], [40, 35]];

        $scope.d1_3 = [[10, 80], [20, 40], [30, 30], [40, 20]];

        $scope.d2 = [];

        for (var i = 0; i < 20; ++i) {
            $scope.d2.push([i, Math.sin(i)]);
        }
        $scope.percent = 90;
        $scope.d3 = [
          { label: "iPhone5S", data: 40 },
          { label: "iPad Mini", data: 10 },
          { label: "iPad Mini Retina", data: 20 },
          { label: "iPhone4S", data: 12 },
          { label: "iPad Air", data: 18 }
        ];

        $scope.refreshData = function () {
            $scope.d0_1 = $scope.d0_2;
        };

        $scope.getRandomData = function () {
            var data = [],
            totalPoints = 150;
            if (data.length > 0)
                data = data.slice(1);
            while (data.length < totalPoints) {
                var prev = data.length > 0 ? data[data.length - 1] : 50,
                  y = prev + Math.random() * 10 - 5;
                if (y < 0) {
                    y = 0;
                } else if (y > 100) {
                    y = 100;
                }
                data.push(y);
            }
            // Zip the generated y values with the x values
            var res = [];
            for (var i = 0; i < data.length; ++i) {
                res.push([i, data[i]])
            }
            return res;
        }

        $scope.d4 = $scope.getRandomData();

    }
]);