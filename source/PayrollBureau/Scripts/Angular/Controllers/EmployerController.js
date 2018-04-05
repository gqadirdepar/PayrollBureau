(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployerController', EmployerController);

    EmployerController.$inject = ['$window', 'EmployerService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function EmployerController($window, EmployerService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.employers = null;
        vm.paging = new Paging();
        vm.orderBy = new OrderBy;
        vm.organisationId = "";
        vm.searchMessage = "";
        vm.initialise = initialise;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.pageChanged = pageChanged;
        return vm;

        function initialise(bureauId) {
            vm.bureauId = bureauId;
            order("Name");
            retrieveEmployer();

        }

        function retrieveEmployer() {
            return EmployerService.retrieveEmployer(vm.bureauId, vm.paging, vm.orderBy).then(
                function (response) {
                    vm.employers = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.employers.length === 0 ? "No Records Found" : "";
                    return vm.employers;
                },
                function () { /*dismissed */ });
        }

        function pageChanged() {
            //vm.tenants = null;
            return retrieveEmployer();
        }

        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveEmployer();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }

    }
})();
