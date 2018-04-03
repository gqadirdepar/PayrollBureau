(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$scope', '$window', 'HomeService'];

    function HomeController($scope, $window, HomeService) {
        var vm = this;
        vm.retrievestatistics = retrievestatistics;
        vm.statistics = [];

        return vm;

        function retrievestatistics() {
            return HomeService.retrievestatistics().then(
               function (response) {
                   vm.statistics = response.data;
                 
                   return vm.statistics;
               },
               function () { /*dismissed */ });
        }



    }
})();
