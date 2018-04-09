using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Interfaces;
using DocumentService.API.RESTClient.Interfaces;
using DocumentService.API.RESTClient.Models;

namespace PayrollBureau.Business.Services
{
    public class DocumentBusinessService : IDocumentBusinessService
    {
        private readonly IPayrollBureauDataService _payrollBureauDataService;
        private readonly IDocumentServiceRestClient _documentServiceRestClient;
        private const string ProductName = "PayrollBureau";

        public DocumentBusinessService(IPayrollBureauDataService payrollBureauDataService, IDocumentServiceRestClient documentServiceRestClient)
        {
            _payrollBureauDataService = payrollBureauDataService;
            _documentServiceRestClient = documentServiceRestClient;
        }

        public ValidationResult<EmployeeDocument> CreateEmployeeDocument(DocumentMeta documentMeta, int employeeId, string userId)
        {
            var validationResult = new ValidationResult<EmployeeDocument>();
            try
            {
                //upload document to document service
                var documentCategoryId = documentMeta.DocumentTypeId;
                var documentCategory = _payrollBureauDataService.Retrieve<DocumentCategory>(e => e.DocumentCategoryId == documentMeta.DocumentTypeId);
                if (documentCategory == null)
                {
                    validationResult.Errors = new List<string> { "Document category not found" };
                    return validationResult;
                }

                var apiDocument = new Document
                {
                    Product = ProductName,
                    Category = documentCategory.FirstOrDefault()?.Name,
                    PayrollId = employeeId.ToString(),
                    CreatedBy = userId,
                    CreatedDateUTC = DateTime.UtcNow
                };

                var document = _documentServiceRestClient.CreateDocument(apiDocument);

                if (document == null)
                {
                    validationResult.Errors = new List<string>() { "Document could not be saved, please try again" };
                    return validationResult;
                }

                var employeeDocument = new EmployeeDocument()
                {
                    DocumentCategoryId = documentCategoryId,
                    DocumentGuid = document.DocumentGuid,
                    EmployeeId = employeeId,
                    Filename = documentMeta.FileName,
                    CreatedBy = userId,
                    Description = documentMeta.Description,
                    CreatedDateUtc = DateTime.UtcNow
                };
                _payrollBureauDataService.Create<EmployeeDocument>(employeeDocument);
                validationResult.Succeeded = true;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Exception = ex;
            }
            return validationResult;
        }

    }
}
