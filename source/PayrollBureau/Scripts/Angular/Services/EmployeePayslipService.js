(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('EmployeePayslipService', EmployeePayslipService);

    EmployeePayslipService.$inject = ['$http'];

    function EmployeePayslipService($http) {
        var service = {
            retrieveEmployeePayslips: retrieveEmployeePayslips
        };

        return service;

        function retrieveEmployeePayslips(employeeId, Paging, OrderBy) {
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