(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployeeController', EmployeeController);

    EmployeeController.$inject = ['$window', 'EmployeeService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function EmployeeController($window, EmployeeService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.employees = [];
        vm.paging = new Paging;
        vm.pageChanged = pageChanged;
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.initialise = initialise;
        vm.employerId;
        vm.bureauId;


        function initialise(bureauId, employerId) {
            vm.orderBy.property = "Name";
            vm.orderBy.direction = "Ascending";
            vm.orderBy.class = "asc";
            vm.employerId = employerId;
            vm.bureauId = bureauId;
            order("Name");
        }

        function retrieveEmployees() {
            return EmployeeService.retrieveEmployees(vm.bureauId, vm.employerId, vm.paging, vm.orderBy)
                .then(function (response) {
                    vm.employees = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.employees.length === 0 ? "No Records Found" : "";
                    return vm.employees;
                });
        }

        function pageChanged() {
            return retrieveEmployees();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEmployees();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
    }
})();
