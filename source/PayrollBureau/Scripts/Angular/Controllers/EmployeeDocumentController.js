(function () {
    'use strict';

    angular
        .module('PayrollBureau')
        .controller('EmployeeDocumentController', EmployeeDocumentController);

    EmployeeDocumentController.$inject = ['$scope', '$window', 'EmployeeDocumentService', 'Paging', 'OrderService', 'OrderBy', 'Order', '$uibModal', 'uiUploader', '$ngBootbox'];

    function EmployeeDocumentController($scope, $window, EmployeeDocumentService, Paging, OrderService, OrderBy, Order, $uibModal, uiUploader, $ngBootbox) {
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
        vm.uploadDocument = uploadDocument;
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

        function uploadDocument() {
            openDocumentModal();
        }

        function openDocumentModal() {
            vm.modalInstance = $uibModal.open({
                size: 'md',
                templateUrl: 'uploadDocument.html',
                controller: ['parent', '$uibModalInstance', 'uiUploader', '$filter', '$scope', function (parent, $uibModalInstance, uiUploader, $filter, $scope) {
                    var $modal = this;

                    $modal.parent = parent;
                    $modal.description = null;

                    $modal.Document = {
                        EmployeeId: parent.employeeId,
                        EmployerId: parent.employerId,
                        BureauId: parent.bureauId
                    };
                    $modal.modalClose = modalClose;
                    $modal.modalSubmit = modalSubmit;
                    $modal.hasErrors = hasErrors;

                    $modal.errorMessages = null;
                    $modal.submitting = false;

                    $modal.selectedFile = null;
                    $modal.onFileChange = function (event) {
                        $modal.selectedFile = null;
                        uiUploader.removeAll();

                        var files = event.target.files;
                        if (files && files.length > 0) {
                            uiUploader.addFiles(files);
                            $scope.$apply(function () {
                                $modal.selectedFile = uiUploader.files[0];
                            });
                        }
                    };


                    function modalClose() { $uibModalInstance.dismiss(); }
                    function modalSubmit() {
                        $modal.errorMessages = [];
                        if (!$modal.selectedFile) $modal.errorMessages.push('File is required.');
                        if ($modal.errorMessages.length > 0) return;
                        $modal.submitting = true;
                        $modal.Document.Description = $modal.description;
                        saveDocument($modal.Document);
                    }
                    function hasErrors() {
                        return $modal.errorMessages && $modal.errorMessages.length > 0;
                    }

                    function saveDocument(document) {

                        uiUploader.startUpload({
                            url: '/Bureaus/' + document.BureauId + '/Employers/' + document.EmployerId + '/Employees/' + document.EmployeeId + "/" + document.Description + '/UploadDocument',
                            concurrency: 1,
                            headers: {
                                'Accept': 'application/json'
                            },
                            onCompleted: function (file, responseText, status) {
                                var responseData = angular.fromJson(responseText);
                                if (responseData.Success) {
                                    $uibModalInstance.close(document);
                                    $window.location.reload();
                                }
                                else {
                                    $scope.$apply(function () {
                                        clearSelectedFile(); //need to clear to let user re-upload the file. uiuploader doesn't work when retrying unless the file is selected again
                                        $modal.errorMessages.push(responseData.Error + ' Please try again.');
                                    });
                                }

                                $scope.$apply(function () {
                                    $modal.submitting = false;
                                });
                            }
                        });
                    }

                    function clearSelectedFile() {
                        angular.element("input[type='file']").val(null);
                        uiUploader.removeAll();
                        $modal.selectedFile = null;
                    }

                }],
                controllerAs: 'model',
                resolve: {
                    parent: function () { return vm; }
                }
            });

            vm.modalInstance.result.then(
                function (document) {
                    vm.retrieveEmployeeDocuments();
                }
                , function () { /*dismissed*/ }
            );
        }

    }
})();
