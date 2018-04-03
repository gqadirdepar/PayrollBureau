(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('EmployerService', EmployerService);

    EmployerService.$inject = ['$http'];

    function EmployerService($http) {
        var service = {
            retrieveEmployer: retrieveEmployer,
        };

        return service;

        function retrieveEmployer(bureauId, paging, orderBy) {
            var url = "/Employer/List",
                data = {
                    bureauId: bureauId,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };

            return $http.post(url, data);
        }     
    }

})();