(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployeePayslipController', EmployeePayslipController);

    EmployeePayslipController.$inject = ['$scope', '$window', 'EmployeePayslipService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function EmployeePayslipController($scope, $window, EmployeePayslipService, Paging, OrderService, OrderBy, Order) {
        var vm = this;
        vm.retrieveEmployeePayslips = retrieveEmployeePayslips;
        vm.employeePayslips = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.initialise = initialise;
        vm.employeeId;
        vm.employerId;
        vm.bureauId;
        return vm;


        function initialise(bureauId, employerId, employeeId) {
            vm.orderBy.property = "EmployeeDocumentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.employeeId = employeeId;
            vm.employerId = employerId;
            vm.bureauId = bureauId;
            order("EmployeeDocumentId");
        }

        function retrieveEmployeePayslips() {
            return EmployeePayslipService.retrieveEmployeePayslips(vm.bureauId, vm.employerId, vm.employeeId, vm.paging, vm.orderBy).then(
               function (response) {
                   vm.employeePayslips = response.data.Items;
                   vm.paging.totalPages = response.data.TotalPages;
                   vm.paging.totalResults = response.data.TotalResults;
                   vm.searchMessage = vm.employeePayslips.length === 0 ? "No Records Found" : "";
                   return vm.employeePayslips;
               },
               function () { /*dismissed */ });
        }

        function pageChanged() {
            return retrieveEmployeePayslips();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEmployeePayslips();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }



    }
})();
