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

        function retrieveEmployeeDocuments(bureauId, employerId, employeeId, Paging, OrderBy) {
            var url = "/Bureaus/" + bureauId + "/Employers/" + employerId + "/Employees/" + employerId + "/Documents",
                data = {
                    bureauId: bureauId,
                    employerId: employerId,
                    employeeId: employeeId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };

            return $http.post(url, data);
        }
    }
})();