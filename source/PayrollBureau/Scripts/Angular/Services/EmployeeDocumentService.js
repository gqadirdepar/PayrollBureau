(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('EmployeeDocumentService', EmployeeDocumentService);

    EmployeeDocumentService.$inject = ['$http'];

    function EmployeeDocumentService($http) {
        var service = {
            retrieveEmployeeDocuments: retrieveEmployeeDocuments
        };

        return service;

        function retrieveEmployeeDocuments(employeeId, Paging, OrderBy) {
            var url = "/Employee/Documents",
                data = {
                    employeeId:employeeId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
            }
    }
})();