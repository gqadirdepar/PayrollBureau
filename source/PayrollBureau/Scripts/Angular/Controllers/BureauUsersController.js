(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('BureauUsersController', BureauUsersController);

    BureauUsersController.$inject = ['$scope', '$window', 'BureauService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function BureauUsersController($scope, $window, BureauService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.bureauId = 0;
        vm.users = null;
        vm.searchMessage = "";
        vm.search = search;
        vm.searchTerm = null;
        vm.init = init;
        vm.paging = new Paging();
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.pageChanged = pageChanged;
        vm.retrieveUsers = retrieveUsers;
        return vm;

        function init(bureauId) {
            vm.bureauId = bureauId;
            order("UserName");
        }

        function retrieveUsers() {
            return BureauService.retrieveUsers(vm.bureauId, vm.searchTerm, vm.paging, vm.orderBy).then(
                function (response) {
                    vm.users = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.users.length === 0 ? "No Records Found" : "";
                    return vm.users;
                },
                function () { /*dismissed */ });
        }

        function pageChanged() {
            vm.bureaus = null;
            return retrieveUsers();
        }


        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveUsers();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
        function search() {
            retrieveUsers();
        }


    }
})();
