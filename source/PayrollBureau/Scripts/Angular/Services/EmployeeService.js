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

        function retrieveEmployees(employerId, Paging, OrderBy) {
            var url = "/Employee/List",
                data = {
                    employerId: employerId,
                    paging: Paging,
                    orderBy: new Array(OrderBy)
                };
            return $http.post(url, data);
        }
    }
})();