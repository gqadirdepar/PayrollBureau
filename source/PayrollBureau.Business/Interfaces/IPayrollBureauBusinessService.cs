using System;
using System.Collections.Generic;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using System.Linq.Expressions;
using PayrollBureau.Business.Models;
using Bureau = PayrollBureau.Business.Models.Bureau;


namespace PayrollBureau.Business.Interfaces
{
    public interface IPayrollBureauBusinessService
    {

        #region Retrieve

        Statistics Retrievestatistics();
        PagedResult<Bureau> RetrieveBureau(string searchTerm, List<OrderBy> orderBy, Paging paging);
        Employer RetrieveEmployer(string aspNetUserId);
        Employer RetrieveEmployer(int employerId);
        PagedResult<Employer> RetrieveEmployer(int bureauId, List<OrderBy> orderBy, Paging paging);
        PagedResult<EmployeeGrid> RetrieveEmployees(Expression<Func<EmployeeGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<DocumentGrid> RetrieveEmployeeDocuments(Expression<Func<DocumentGrid, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Employee RetrieveEmployee(int employeeId);
        Bureau RetrieveBureau(int bureauId);
        Bureau RetrieveBureau(string aspNetUserId);
        PagedResult<AspNetUser> RetrieveBureauUsers(int bureauId, string searchTerm, List<OrderBy> orderBy, Paging paging);
        BureauStatistics RetrieveBureauStatistics(int bureauId);
        ValidationResult<Employee> EmployeeAlreadyExists(string name, int? employeeId);
        #endregion

        #region create

        ValidationResult<Employer> CreateEmployer(Employer employer);
        AspNetUserBureau CreateAspNetUserBureau(int bureauId, string aspNetUserId);
        AspNetUserEmployer CreateAspNetUserEmployer(int employerId, string aspNetUserId);
        ValidationResult<Employee> CreateEmployee(Employee employee);
        ValidationResult<Bureau> CreateBureau(Bureau bureau);
        #endregion

        #region Update
        ValidationResult<Employer> UpdateEmployer(Employer employer);
        ValidationResult<Employee> UpdateEmployee(Employee employer);
        ValidationResult<Bureau> UpdateBureau(Bureau bureau);
        #endregion  
    }
}
