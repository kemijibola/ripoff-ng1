'use strict';

app.factory('wizardDataService', ['$http', '$q', function ($http, $q) {
    var wizardServiceFactory = {};


    var _newReport = {
        isAuth: false,
            company: "",
            alias: "",
            webSite: "",
            locationTypeId: 0,
            stateId: 0,
            cityId: 0,
            phoneNo: "",
            Email: "",
            topicId: 0,
            categoryId: 0,
            reportText: "",
            onlineTransaction: false,
            creditCard: false,
            Street: "",
            fax: "",
            reportId: 0,
            ripTerms: false,
            contactbyMedia: false
                    
    };


    wizardServiceFactory.report = _newReport;

    return wizardServiceFactory;
}]);