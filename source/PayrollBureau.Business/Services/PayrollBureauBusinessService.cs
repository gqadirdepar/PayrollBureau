using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Data.Interfaces;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;

namespace PayrollBureau.Business.Services
{
    public class PayrollBureauBusinessService : IPayrollBureauBusinessService
    {
        private readonly IPayrollBureauDataService _payrollBureauDataService;

        public PayrollBureauBusinessService(IPayrollBureauDataService payrollBureauDataService)
        {
            _payrollBureauDataService = payrollBureauDataService;
        }

        #region Retrieve

        public Statistics Retrievestatistics()
        {
            var result = _payrollBureauDataService.Retrieve<Bureau>(p => true).ToList();
            return new Statistics
            {
                Bureau = result.Count,
                Users = result.Count(u => !string.IsNullOrEmpty(u.AspnetUserId))
            };
        }
        public PagedResult<Bureau> RetrieveBureau(string searchTerm, List<OrderBy> orderBy, Paging paging)
        {
            if (!string.IsNullOrEmpty(searchTerm))
                return _payrollBureauDataService.RetrievePagedResult<Bureau>(e => e.Name.Contains(searchTerm), orderBy, paging);
            return _payrollBureauDataService.RetrievePagedResult<Bureau>(e => true, orderBy, paging);
        }


        public Employer RetrieveEmployer(string aspNetUserId)
        {
            return _payrollBureauDataService.Retrieve<Employer>(e => e.AspnetUserId == aspNetUserId).FirstOrDefault();
        }

        public Employer RetrieveEmployer(int employerId)
        {
            return _payrollBureauDataService.Retrieve<Employer>(e => e.EmployerId == employerId, e => e.Bureau).FirstOrDefault();
        }

        public PagedResult<Employer> RetrieveEmployer(int bureauId, List<OrderBy> orderBy, Paging paging)
        {
            if (paging == null)
                return _payrollBureauDataService.RetrievePagedResult<Employer>(t => t.BureauId == bureauId);
            return _payrollBureauDataService.RetrievePagedResult<Employer>(t => t.BureauId == bureauId, orderBy, paging);
        }

        public PagedResult<EmployeeGrid> RetrieveEmployees(Expression<Func<EmployeeGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _payrollBureauDataService.RetrievePagedResult(predicate, orderBy, paging);
        }

        public PagedResult<EmployeeDocument> RetrieveEmployeeDocuments(Expression<Func<EmployeeDocument, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _payrollBureauDataService.RetrievePagedResult(predicate, orderBy, paging);
        }

        public Employee RetrieveEmployee(int employeeId)
        {
            return _payrollBureauDataService.Retrieve<Employee>(e => e.EmployeeId == employeeId, e => e.Employer, e => e.Employer.Bureau).FirstOrDefault();
        }

        public Bureau RetrieveBureau(int bureauId)
        {
            return _payrollBureauDataService.Retrieve<Bureau>(bureauId);

        }

        public Bureau RetrieveBureau(string aspNetUserId)
        {
            return _payrollBureauDataService.Retrieve<Bureau>(e => e.AspnetUserId == aspNetUserId).FirstOrDefault();

        }

        #endregion


        #region Create
        public ValidationResult<Employer> CreateEmployer(Employer employer)
        {
            var validationResult = new ValidationResult<Employer>();
            //if (!validationResult.Succeeded)
            //    return validationResult;
            try
            {
                validationResult.Entity = _payrollBureauDataService.Create(employer); ;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;

        }
        #endregion

        #region Helper
        public ValidationResult<Employer> EmployerAlreadyExists(string name,string email, int? employerId)
        {
            var alreadyExists = _payrollBureauDataService.Retrieve<Employer>(p => p.Name.ToLower() == name.ToLower() && p.EmployerId != (employerId ?? -1)).Any();
            return new ValidationResult<Employer>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { $"Employer name already exists." } : null
            };
        }
        #endregion
    }
}
