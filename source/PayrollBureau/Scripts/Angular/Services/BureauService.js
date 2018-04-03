(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .factory('BureauService', BureauService);

    BureauService.$inject = ['$http'];

    function BureauService($http) {
        var service = {
           
            retrieveBureau: retrieveBureau
           

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

     
    }
})();