(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('BureauController', BureauController);

    BureauController.$inject = ['$scope', '$window', 'BureauService', 'Paging', 'OrderService', 'OrderBy', 'Order'];

    function BureauController($scope, $window, BureauService, Paging, OrderService, OrderBy, Order) {
        /* jshint validthis:true */
        var vm = this;
        vm.bureaus = null;
        vm.searchMessage = "";
        vm.search = search;
        vm.searchTerm = null;
        vm.init = init;
        vm.paging = new Paging();
        vm.orderBy = new OrderBy;
        vm.order = order;
        vm.orderClass = orderClass;
        vm.pageChanged = pageChanged;
        vm.retrieveBureau = retrieveBureau;
        vm.Errors = [];
        return vm;

        function init() {
            order("Name");
        }

        function retrieveBureau() {
            return BureauService.retrieveBureau(vm.searchTerm, vm.paging, vm.orderBy).then(
                function (response) {
                    vm.bureaus = response.data.Items;
                    vm.paging.totalPages = response.data.TotalPages;
                    vm.paging.totalResults = response.data.TotalResults;
                    vm.searchMessage = vm.bureaus.length === 0 ? "No Records Found" : "";
                    return vm.bureaus;
                },
                function () { /*dismissed */ });
        }

        function pageChanged() {
            vm.bureaus = null;
            return retrieveBureau();
        }


        function order(property) {
            vm.orderBy = OrderService.order(vm.orderBy, property);
            return retrieveBureau();
        }

        function orderClass(property) {
            return OrderService.orderClass(vm.orderBy, property);
        }
        function search() {
            retrieveBureau();
        }


    }
})();
