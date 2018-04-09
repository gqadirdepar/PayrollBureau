(function (angular) {
    'use strict';

    angular
        .module('PayrollBureau')
        .directive('fileUploadOnChange', fileUploadOnChange);

    function fileUploadOnChange() {
        var directive = {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var onChangeHandler = scope.$eval(attrs.fileUploadOnChange);
                element.bind('change', onChangeHandler);
            }
        };

        return directive;
    }
})(window.angular);