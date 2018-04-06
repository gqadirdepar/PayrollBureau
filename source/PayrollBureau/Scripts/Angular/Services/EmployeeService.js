(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('EmployeeService', EmployeeService);

    EmployeeService.$inject = ['$http'];

    function EmployeeService($http) {
        var service = {
            retrieveEmployees: retrieveEmployees
        };

        return service;

        function retrieveEmployees(bureauId, employerId, Paging, OrderBy) {
            var url = "/Bureaus/" + bureauId + "/Employers/" + employerId + "/Employees/List",
                data = {
                    bureauId: bureauId,
                    employerId: employerId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();