(function () {

    angular.module('app')
        .factory('infraDataService', ['$q', '$http', '$cacheFactory','wizardDataService', infraDataService]);


    function infraDataService($q, $http, $cacheFactory,wizardDataService) {
        var userid = "";
        var useridF = "";
        var report = [];
        var reportOwnerId = null;
        var hasLiked = false;
        var usernames = [];
        var username = null;
        
        return {
            
            useridF: useridF,
            getStateByCountry: getStateByCountry,
            getCityByState: getCityByState,
            getCountry: getCountry,
            addUserDetails: addUserDetails,
            GetUserIdByUsernamePassword: GetUserIdByUsernamePassword,
            getLatestReport: getLatestReport,
            getState: getState,
            getCity:getCity,
            getLocation: getLocation,
            getCategoryByTopic: getCategoryByTopic,
            getCityByState: getCityByState,
            getTopic: getTopic,
            getLatestLawFirmReview: getLatestLawFirmReview,
            getAllReportByDate: getAllReportByDate,
            getReportDetailById: getReportDetailById,
            GetUserNameById: GetUserNameById,
            getLawCategoryByFirm: getLawCategoryByFirm,
            savetrackUser: savetrackUser,
            saveRebuttal: saveRebuttal,
            getUserActivity: getUserActivity,
            getLawFirmDetails: getLawFirmDetails,
            getFirmCategory: getFirmCategory,
            getFirmLike: getFirmLike,
            saveFeedback:saveFeedback,
            updateRipOffFirm: updateRipOffFirm,
            getLawCategory: getLawCategory,
            getFeedback:getFeedback,
            savebugError: savebugError,
            getReportByUserId: getReportByUserId,
            updateUserAccount: updateUserAccount,
            getALLLawfirm: getALLLawfirm,
            forgotPassword: forgotPassword,
            getFirmRegion:getFirmRegion,
            getLawFirmById: getLawFirmById,
            GetTypeOfUserById:GetTypeOfUserById,
            saveClientMeetingDetails:saveClientMeetingDetails,
            GetUserIdByUsername: GetUserIdByUsername,
            GetRegisteredUsers: GetRegisteredUsers,
            getLawfirmByFirmPreference: getLawfirmByFirmPreference,
            getReportByUserIdInClientInitiation: getReportByUserIdInClientInitiation,
            getBanks: getBanks,
            resetPassword: resetPassword,
            saveTransactionDetails: saveTransactionDetails,
            saveHubRole: saveHubRole,
            addUserRoleDetails: addUserRoleDetails,
            getAllRoles: getAllRoles,
            RemoveUserFromRole: RemoveUserFromRole,
            AddUserToRole: AddUserToRole,
            saveClient: saveClient,
            GetReporByStatusApproved: GetReporByStatusApproved

        };
        function saveClient(newClient) {

            return $http.post('api/Client', newClient, {
            })
            .then(function (response) {
                return response.data
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function AddUserToRole(addModel) {

            return $http.post('api/roles/AddUserToRole/',addModel, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('User could not be added to role(HTTP status: ' + error.status + ')');
            });
        }
        function RemoveUserFromRole(removeModel) {

            return $http.post('api/roles/RemoveUserFromRole', removeModel, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('User could not be removed from role(HTTP status: ' + error.status + ')');
            });
        }
        function resetPassword(resetP) {

            return $http.post('api/accounts/ResetPassword', resetP, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error resetting password. (HTTP status: ' + error.status + ')');
            });
        }
        function addUserRoleDetails(user) {
            return $http.put('api/accounts/AssignRolesToUser/', { roles: user.roles }, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function saveHubRole(model) {
            var nameMode = { name: model }
            return $http({
                method: 'POST',
                data: JSON.stringify(nameMode),
                contentType: "application/json",
                url: 'api/roles/Create'
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error. (HTTP status: ' + error.status + ')');
            });
        }
        function getAllRoles() {
            return $http({
                method: 'GET',
                url: 'api/roles/GetAllRoles',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLatestReport() {
            return $http({
                method: 'GET',
                url: 'api/report/',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getBanks() {
            return $http({
                method: 'GET',
                url: 'api/bank/',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLatestLawFirmReview() {
            return $http({
                method: 'GET',
                url: 'api/firmcomment',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getCountry() {
            return $http({
                method: 'GET',
                url: 'api/country',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getState() {
            return $http({
                method: 'GET',
                url: 'api/state',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getCity() {
            return $http({
                method: 'GET',
                url: 'api/city',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLawCategoryByFirm() {
            return $http({
                method: 'GET',
                url: 'api/ripofffirm',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getFirmRegion() {
            return $http({
                method: 'GET',
                url: 'api/firmregion',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLawfirmByFirmPreference(categoryId) {
            return $http({
                method: 'GET',
                url: 'api/lawcategory/' + categoryId
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getALLLawfirm() {
            return $http({
                method: 'GET',
                url: 'api/lawcategory/true',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function GetReporByStatusApproved() {
            return $http({
                method: 'GET',
                url: 'api/report/GetReporByStatusApproved/true/1',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getAllReportByDate() {
            return $http({
                method: 'GET',
                url: 'api/report/GetAllReportByDate/true',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getTopic() {
            return $http({
                method: 'GET',
                url: 'api/topic',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getFeedback() {
            return $http({
                method: 'GET',
                url: 'api/feedback',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLocation() {
            return $http({
                method: 'GET',
                url: 'api/locationtype',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLawCategory() {
            return $http({
                method: 'GET',
                url: 'api/lawcategory',
            })
                .then(sendResponseData)
                .catch(sendGetError);
        }
        function getLawFirmDetails(lawfirmId) {
            return $http({
                method: 'GET',
                url: 'api/ripofffirm/' + lawfirmId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getFirmCategory(lawfirmId) {
            return $http({
                method: 'GET',
                url: 'api/firmcategory/' + lawfirmId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getFirmLike(firmId) {
            return $http({
                method: 'GET',
                url: 'api/ripofffirm/GetFirm/' + firmId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getCityByState(id) {
            return $http({
                method: 'GET',
                url: 'api/city/' + id
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getCategoryByTopic(id) {
            return $http({
                method: 'GET',
                url: 'api/category/' + id
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getStateByCountry(id) {
            return $http({
                method: 'GET',
                url: 'api/state/' + id
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getLawFirmById(id) {
            return $http({
                method: 'GET',
                url: 'api/lawfirm/' + id
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getReportDetailById(id) {
            return $http({
                method: 'GET',
                url: 'api/report/' + id
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getReportByUserId(userId) {
            return $http({
                method: 'GET',
                url: 'api/report/GetReportByUserId/' + userId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function getReportByUserIdInClientInitiation(userId) {
            return $http({
                method: 'GET',
                url: 'api/clientinitiatemeeting/getReportByUserIdInClientInitiation/' + userId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function updateRipOffFirm(firmId) {

            return $http({
                method: 'PUT',
                url: 'api/ripofffirm/',
                params:{id:firmId}
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error updating record. (HTTP status: ' + error.status + ')');
            });
        }
        function getUserActivity(username, firmid) {
            return $http({
                method: 'GET',
                url: 'api/trackuser/GetUserActivityByUsername/' + username + "/" + firmid
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function GetUserIdByUsernamePassword(username, password) {
            return $http({
                method: 'GET',
                url: 'api/accounts/GetUserIdByUsernamePassword/' + username + "/" + password
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function GetUserIdByUsername(username) {
            return $http({
                method: 'GET',
                url: 'api/accounts/GetUserIdByUsername/' + username + "/" + true
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function GetTypeOfUserById(userid) {
            return $http({
                method: 'GET',
                url: 'api/accounts/GetTypeOfUserById/' + true + "/" + userid
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function GetUserNameById(userId) {
            return $http({
                method: 'GET',
                url: 'api/accounts/GetUserNameById/' + userId
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function GetRegisteredUsers() {
            return $http({
                method: 'GET',
                url: 'api/accounts/users/'
            })
           .then(sendResponseData)
           .catch(sendGetError);
        }
        function updateUserAccount(editUser) {

            return $http.post('api/accounts/UpdateUser', editUser, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function savebugError(newError) {

            return $http.post('api/reportbug', newError, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function saveClientMeetingDetails(newMeeting) {

            return $http.post('api/ClientMeetingRequest', newMeeting, {
            })
            .then(function (response) {
                return response.data
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function saveTransactionDetails(newTrans) {

            return $http.post('api/transaction', newTrans, {
            })
            .then(function (response) {
                return response.data
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function saveFeedback(newFeedback) {

            return $http.post('api/feedback', newFeedback, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function saveRebuttal(newRebuttal) {

            return $http.post('api/rebuttal', newRebuttal, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function forgotPassword(model) {
            var emailMode  = {email: model}
            return $http({
                method: 'POST',
                data: JSON.stringify(emailMode),
                contentType: "application/json",
                url: 'api/accounts/ForgotPassword'
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error. (HTTP status: ' + error.status + ')');
            });
        }
        function savetrackUser(newTrack) {

            return $http.post('api/trackuser', newTrack, {
            })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                return $q.reject('Error adding record. (HTTP status: ' + error.status + ')');
            });
        }
        function addUserDetails(newUser) {

            return $http.post('api/accounts/CreateUser', newUser, {
            })
            .then(addUserDetailsSuccess)
            .catch(addUserDetailsError);
        }
        function addUserDetailsSuccess(response) {

            return response.data;
            console.log(response.data);

        }
        function addUserDetailsError(response) {
           return $q.reject(response.status);
        }

        function sendResponseData(response) {
            return response.data;
        }

        function sendGetError(response) {

            return $q.reject('Error retrieving data(s). (HTTP status: ' + response.status + ')');

        }
    }

}());