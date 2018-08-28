(function () {
    'use strict';

    angular
        .module('app')
        .factory('photoManagerClient', photoManagerClient);

    photoManagerClient.$inject = ['$resource', 'wizardDataService','infraDataService'];


    function photoManagerClient($resource, wizardDataService,infraDataService) {
        return $resource("api/reportimage/:fileName",
        {
            id: "@fileName"
        },
        {
            'query':
            {
                method: 'GET'
            },
            'save':
                {
                    method: 'POST',
                    transformRequest: angular.identity,
                    headers: {
                        'Content-Type': undefined

                    },
                    params: {report: wizardDataService,userid: infraDataService.useridF }
                    
                    
                    },
            'remove':
                {
                    method: 'DELETE', url: 'api/reportimage/:fileName', params:
                        {
                            name: '@fileName'

                        }
                }
        });
    }
})();