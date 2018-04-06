(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployeeDocumentController', EmployeeDocumentController);

    EmployeeDocumentController.$inject = ['$scope', '$window', 'EmployeeDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function EmployeeDocumentController($scope, $window, EmployeeDocumentService, Paging, OrderService, OrderBy, Order) {
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

        function retrieveEmployeeDocuments() {
            return EmployeeDocumentService.retrieveEmployeeDocuments(vm.bureauId, vm.employerId, vm.employeeId, vm.paging, vm.orderBy).then(
               function (response) {
                   vm.employeeDocuments = response.data.Items;
                   vm.paging.totalPages = response.data.TotalPages;
                   vm.paging.totalResults = response.data.TotalResults;
                   vm.searchMessage = vm.employeeDocuments.length === 0 ? "No Records Found" : "";
                   return vm.employeeDocuments;
               },
               function () { /*dismissed */ });
        }

        function pageChanged() {
            return retrieveEmployeeDocuments();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEmployeeDocuments();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }



    }
})();
