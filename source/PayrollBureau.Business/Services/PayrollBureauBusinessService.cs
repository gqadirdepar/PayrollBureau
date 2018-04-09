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
                //Users = result.Count(u => !string.IsNullOrEmpty(u.AspnetUserId))
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
            //return _payrollBureauDataService.Retrieve<Employer>(e => e.asp).FirstOrDefault();
            return null;
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

        public PagedResult<DocumentGrid> RetrieveEmployeeDocuments(Expression<Func<DocumentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
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
            //return _payrollBureauDataService.Retrieve<Bureau>(e => e.AspnetUserId == aspNetUserId).FirstOrDefault();
            return null;
        }
        public PagedResult<AspNetUser> RetrieveBureauUsers(int bureauId, string searchTerm, List<OrderBy> orderBy, Paging paging)
        {

            var data = _payrollBureauDataService.RetrievePagedResultBureauUsers<AspNetUser>(bureauId, searchTerm,
                orderBy, paging);
            return data;
        }

        public BureauStatistics RetrieveBureauStatistics(int bureauId)
        {
            var result = _payrollBureauDataService.Retrieve<Bureau>(b => b.BureauId == bureauId, e => e.Employers, i => i.AspNetUserBureaus).ToList().FirstOrDefault();
            return new BureauStatistics
            {
                Employer = result?.Employers.Count ?? 0,
                User = result?.AspNetUserBureaus.Count ?? 0

            };
        }
        #endregion


        #region Create
        public ValidationResult<Employer> CreateEmployer(Employer employer)
        {
            var validationResult = EmployerAlreadyExists(employer.Name, null);
            if (!validationResult.Succeeded)
                return validationResult;
            try
            {
                employer.CreatedDateUtc = DateTime.UtcNow;
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

        public AspNetUserBureau CreateAspNetUserBureau(int bureauId, string aspNetUserId)
        {
            var aspNetUserBureau = new AspNetUserBureau
            {
                BureauId    = bureauId,
                AspNetUserId = aspNetUserId
            };
            
            return _payrollBureauDataService.Create(aspNetUserBureau);
        }

        public AspNetUserEmployer CreateAspNetUserEmployer(int employerId, string aspNetUserId)
        {
            var aspNetUserEmployer = new AspNetUserEmployer
            {
                EmployerId = employerId,
                AspNetUserId = aspNetUserId
            };

            return _payrollBureauDataService.Create(aspNetUserEmployer);
        }

        public ValidationResult<Employee> CreateEmployee(Employee employee)
        {
            var validationResult = new ValidationResult<Employee>();
            try
            {
                employee.CreatedDateUtc = DateTime.UtcNow;
                validationResult.Entity = _payrollBureauDataService.Create(employee); ;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;

        }

        public ValidationResult<Bureau> CreateBureau(Bureau bureau)
        {
            var validationResult = BureauAlreadyExists(bureau.Name, null);
            if (!validationResult.Succeeded)
                return validationResult;
            try
            {

                bureau.CreatedDateUtc = DateTime.UtcNow;
                validationResult.Entity = _payrollBureauDataService.Create(bureau); ;
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
        public ValidationResult<Employer> EmployerAlreadyExists(string name, int? employerId)
        {
            var alreadyExists = _payrollBureauDataService.Retrieve<Employer>(p => p.Name.ToLower() == name.ToLower() && p.EmployerId != (employerId ?? -1)).Any();
            return new ValidationResult<Employer>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { $"Employer name already exists." } : null
            };
        }

        public ValidationResult<Employee> EmployeeAlreadyExists(string name, int? employeeId)
        {
            var alreadyExists = _payrollBureauDataService.Retrieve<Employee>(p => p.Name.ToLower() == name.ToLower() && p.EmployeeId != (employeeId ?? -1)).Any();
            return new ValidationResult<Employee>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { $"Employee name already exists." } : null
            };
        }

        public ValidationResult<Bureau> BureauAlreadyExists(string name, int? bureauId)
        {
            var alreadyExists = _payrollBureauDataService.Retrieve<Bureau>(p => p.Name.ToLower() == name.ToLower() && p.BureauId != (bureauId ?? -1)).Any();
            return new ValidationResult<Bureau>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { $"Bureau name already exists." } : null
            };
        }
        #endregion

        #region update
        public ValidationResult<Bureau> UpdateBureau(Bureau bureau)
        {
            var validationResult = BureauAlreadyExists(bureau.Name, bureau.BureauId);
            if (!validationResult.Succeeded)
                return validationResult;
            try
            {
                var bureauData = RetrieveBureau(bureau.BureauId);
                bureauData.Name = bureau.Name;
                validationResult.Entity = _payrollBureauDataService.UpdateEntityEntry(bureauData);
                validationResult.Succeeded = true;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;
        }

        public ValidationResult<Employer> UpdateEmployer(Employer employer)
        {
            var validationResult = EmployerAlreadyExists(employer.Name, employer.EmployerId);
            if (!validationResult.Succeeded)
                return validationResult;
            try
            {
                var employerData = RetrieveEmployer(employer.EmployerId);
                employerData.Name = employer.Name;
                employerData.Address1 = employer.Address1;
                employerData.Address2 = employer.Address2;
                employerData.Address3 = employer.Address3;
                employerData.Address4 = employer.Address4;
                validationResult.Entity = _payrollBureauDataService.UpdateEntityEntry(employerData);
                validationResult.Succeeded = true;
                return validationResult;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Message = ex.Message;
            }
            return validationResult;
        }

        public ValidationResult<Employee> UpdateEmployee(Employee employee)
        {
            var validationResult = EmployeeAlreadyExists(employee.Name, employee.EmployeeId);
            if (!validationResult.Succeeded)
                return validationResult;
            try
            {
                var employeeData = RetrieveEmployee(employee.EmployeeId);
                employeeData.Name = employee.Name;
                employeeData.ProductName = employee.ProductName;
                employeeData.PayrollNumber = employee.PayrollNumber;
                validationResult.Entity = _payrollBureauDataService.UpdateEntityEntry(employeeData);
                validationResult.Succeeded = true;
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
    }
}
