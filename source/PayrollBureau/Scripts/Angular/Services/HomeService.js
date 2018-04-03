(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('HomeService', HomeService);

    HomeService.$inject = ['$http'];

    function HomeService($http) {
        var service = {
            retrievestatistics: retrievestatistics
        };

        return service;

        function retrievestatistics() {
            var url = "/Home/Statistics",
                data = {
                };

            return $http.post(url, data);
            }
    }
})();