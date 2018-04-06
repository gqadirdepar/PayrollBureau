(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('BureauService', BureauService);

    BureauService.$inject = ['$http'];

    function BureauService($http) {
        var service = {
            retrieveBureau: retrieveBureau,
            retrieveUsers: retrieveUsers,
            retrieveStatistics: retrieveStatistics


    };

        return service;

        function retrieveBureau(searchTerm, paging, orderBy) {
            var url = "/Bureau/List",
                data = {
                    searchTerm: searchTerm,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };

            return $http.post(url, data);
        }
        function retrieveUsers(bureauId,searchTerm, paging, orderBy) {
            var url = "/Bureaus/BureauUsers/List",
                data = {
                    bureauId:bureauId,
                    searchTerm: searchTerm,
                    paging: paging,
                    orderBy: new Array(orderBy)
                };

            return $http.post(url, data);
        }

        function retrieveStatistics(bureauId) {
            var url = "/Bureau/Statistics",
                data = {
                    bureauId: bureauId
                };

            return $http.post(url, data);
        }

     
    }
})();