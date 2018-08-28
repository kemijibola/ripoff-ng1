app.controller('LawFirmCtrl', ['$scope', '$state', '$http', '$stateParams', 'infraDataService', function ($scope, state, http, $stateParams, infraDataService) {

    $scope.lawbyfirmcategories = [];
    $scope.label = $stateParams.label;
    $scope.lawyerPreference = [];
    $scope.reviews = [];
    $scope.lawCategoryId = 0;
   // $scope.reportExists = false;
    $scope.labels = [

      { name: 'Bail Bonds', filter: 'bailbonds', color: '#23b7e5' },
      { name: 'Arbitrate', filter: 'arbitrate', color: '#7266ba' },
      { name: 'Administrative Law', filter: 'administrative', color: '#fad733' },
      { name: 'Antitrust & Trade Regulatin', filter: 'antitrust', color: '#27c24c' }
    ];

    $scope.onSelected = function (item) {
        $scope.lawCategoryId = item.id;
    };
    $scope.contactfirm = {
        reportId: ""
    };

    $scope.myPromise = infraDataService.getLawCategoryByFirm()
        .then(function (result) {
            $scope.lawbyfirmcategories = result;
            
        });


    infraDataService.getLatestLawFirmReview()
        .then(function (result) {
            $scope.reviews = result;
        },
        function (fault) {

        });

    $scope.labelClass = function (label) {
        return {
            'b-l-info': angular.lowercase(label) === 'bailbonds',
            'b-l-primary': angular.lowercase(label) === 'arbitrate',
            'b-l-warning': angular.lowercase(label) === 'administrative',
            'b-l-success': angular.lowercase(label) === 'antitrust'
        };
    };

}]);

//app.controller('LawFirmListCtrl', ['$scope', '$state', '$http', '$stateParams', 'infraDataService', function ($scope, state, http, $stateParams, infraDataService) {


//    $scope.lawbyfirmcategories = [];
//    $scope.label = $stateParams.label;

//    $scope.myPromise = infraDataService.getLawCategoryByFirm()
//    .then(function (result) {
//        $scope.lawbyfirmcategories = result;
//    });

//}]);

app.controller('LawFirmDetailCtrl', ['$scope', '$http', '$stateParams', 'infraDataService', 'authService', '$modal', '$log', function ($scope, $http, $stateParams, infraDataService, authService, $modal, $log) {

    $scope.lawfirmDetails = [];
    $scope.userActivity = {};
    $scope.newFirmLike = 0;
    
    

    var currentuser = "";
    var firmId = 0;

    $scope.hasLiked = true;

    $scope.like = 0;
    $scope.firmLike = 0;
    var checkLiked = false;

    $scope.Isusername = authService.authentication.userName;
    var username = authService.authentication.userName;
    var isLoggedIn = authService.authentication.isAuth;

    $scope.newtrackUser = {
        userName: "",
        hasLiked: false,
        firmId: 0
    }

    $scope.items = ['item1', 'item2', 'item3'];
    $scope.open = function (size) {
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });


        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.saveFirmReview = function () {
        $scope.newfirmReiew.userName = username
        $scope.newfirmReiew.firmId = $stateParams.lawfirmId;
        infraDataService.savetrackUser($scope.newfirmReiew)
            .then(function (data) {

            });
    };


    $scope.UserClickButton = function (hasUserLiked) {
        infraDataService.getUserActivity($scope.Isusername, $stateParams.lawfirmId)
            .then(function (result) {

                try {
                    if (result == "") {
                        $scope.hasLiked = false;
                        firmId = $stateParams.lawfirmId;

                    }
                    else {
                        
                       username = result[0].userName;
                       firmId = result[0].firmId;
                       $scope.hasLiked = result[0].hasLiked;
                    }

                    if (firmId == $stateParams.lawfirmId && $scope.hasLiked == true) {

                        return $scope.hasLiked = true;
                    }
                    else
                        return $scope.hasLiked = false;

                }
                catch (e) {

                }
            })
    };
    $scope.UserClickButton();

    $scope.firmCategory = [];
    $scope.myPromise = infraDataService.getLawFirmDetails($stateParams.lawfirmId)
            .then(function (result) {
                $scope.lawfirmDetails = result;
                $scope.like = $scope.lawfirmDetails[0].ripOffFirm.firmlike;
            },
            function () {
            });


    infraDataService.getFirmCategory($stateParams.lawfirmId)
            .then(function (response) {
                $scope.firmCategory = response
            });


    $scope.setFirmLike = function () {
        infraDataService.getFirmLike($stateParams.lawfirmId)
        .then(function (result) {
            $scope.firmLike = result;
        });
    };
    $scope.setFirmLike();

    $scope.update = function () {
        infraDataService.updateRipOffFirm($stateParams.lawfirmId)
           .then(function (result) {
               $scope.lawfirmDetails[0].ripOffFirm.firmlike = result.firmlike;
             //  $scope.like = result.data.firmlike;
               updateUserActivity();
           }, function (error) {

           });
    };

    var updateUserActivity = function () {
        $scope.newtrackUser.userName = username
        $scope.newtrackUser.hasLiked = true;
        $scope.newtrackUser.firmId = $stateParams.lawfirmId;
        infraDataService.savetrackUser($scope.newtrackUser)
            .then(function (response) {
                $scope.hasLiked = true;
                //$scope.UserClickButton();
            },
            function (response) {

            });
    };


}]);

app.controller('MailNewCtrl', ['$scope', function ($scope) {
    $scope.mail = {
        to: '',
        subject: '',
        content: ''
    }
    $scope.tolist = [
      { name: 'James', email: 'james@gmail.com' },
      { name: 'Luoris Kiso', email: 'luoris.kiso@hotmail.com' },
      { name: 'Lucy Yokes', email: 'lucy.yokes@gmail.com' }
    ];
}]);

angular.module('app').directive('labelColor', function () {
    return function (scope, $el, attrs) {
        $el.css({ 'color': attrs.color });
    }
});