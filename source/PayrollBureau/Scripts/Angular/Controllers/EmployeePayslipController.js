(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployeePayslipController', EmployeePayslipController);

    EmployeePayslipController.$inject = ['$scope', '$window', 'EmployeePayslipService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function EmployeePayslipController($scope, $window, EmployeePayslipService, Paging, OrderService, OrderBy, Order) {
        var vm = this;
        vm.retrieveEmployeeDocuments = retrieveEmployeeDocuments;
        vm.employeeDocuments = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.initialise = initialise;
        vm.employeeId;
        return vm;


        function initialise(employeeId) {
            vm.orderBy.property = "EmployeeDocumentId";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.employeeId = employeeId;
            order("EmployeeDocumentId");
        }

        function retrieveEmployeePayslips() {
            return EmployeePayslipService.retrieveEmployeePayslips(vm.employeeId, vm.paging, vm.orderBy).then(
               function (response) {
                   vm.employeeDocuments = response.data.Items;
                   return vm.employeeDocuments;
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
