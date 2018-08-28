app.controller('AdminRoleCtr', ['$scope', '$location', 'infraDataService', '$stateParams', '$state', 'localStorageService', 'searchService', function ($scope, $location, infraDataService, $stateParams, $state, localStorageService,searchService) {


    $scope.userName = "";
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.userId = $stateParams.param;
    $scope.roles = [];
    $scope.isSaving = false;
    var userid = $stateParams.param;
    $scope.selection = "";
    $scope.savedSuccessfully = false;
   
    infraDataService.GetUserNameById($scope.userId)
        .then(function (data) {
            $scope.userName = data;
        })

    $scope.onRoleChecked = function (role) {
        if (role.isChecked) {
           // console.log("check");
            addRoleToUser($scope.userId, role.name);
        } else {
           // console.log("uncheck");
            RemoveUserFromRole($scope.userId, role.name);
        }

    };

    //}
    //$scope.onRoleChecked = function (role) {
    //    $scope.selection = role.name;
    //    if ($scope.role.isChecked) {
    //        addRoleToUser($scope.userId, $scope.selection);
    //    }
    //    else
    //    {

    //            console.log("uncheck");
    //        //RemoveUserFromRole($scope.userId, $scope.selection);
          
    //        //console.log($scope.userId);
    //        //console.log($scope.selection);
    //    }
    //       // $scope.savedSuccessfully = false;
    //    //} else {
    //    //    removeFromRole($scope.userId, $scope.selection);
    //    //    console.log($scope.selection);
    //    //}

    //};
    var addRoleToUser = function (userId, role) {
        $scope.userRole = {};
        $scope.userRole.UserId = $scope.userId;
        $scope.userRole.Roles = role;
        
        infraDataService.AddUserToRole($scope.userRole)
            .then(function (response) {
            }, function (fault) {
            });
    };
    var RemoveUserFromRole = function (userId, role) {
        $scope.userRole = {};
        $scope.userRole.UserId = $scope.userId;
        $scope.userRole.Roles = role;
        infraDataService.RemoveUserFromRole($scope.userRole)
            .then(function (response) {
            }, function (fault) {
            });
    };

    infraDataService.getAllRoles().then(function (data) {
        $scope.result = data;
        for (var i = 0; i < $scope.result.length; i++) {
            $scope.role = $scope.result[i];
            $scope.role = { id: $scope.role.id, name: $scope.role.name, isChecked: "false" };
            $scope.roles.push($scope.role);
        }
    });
    
}])
;