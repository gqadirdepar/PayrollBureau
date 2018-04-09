using System;
using System.Collections.Generic;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using System.Linq.Expressions;
using PayrollBureau.Business.Models;


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
        ValidationResult<Employer> EmployerAlreadyExists(string name,string email, int? employerId);
        PagedResult<AspNetUser> RetrieveBureauUsers(int bureauId, string searchTerm, List<OrderBy> orderBy,Paging paging);
        BureauStatistics RetrieveBureauStatistics(int bureauId);
        ValidationResult<Employer> EmployerAlreadyExists(string name, int? employerId);
        ValidationResult<Employee> EmployeeAlreadyExists(string name, int? employeeId);
        ValidationResult<Bureau> BureaurAlreadyExists(string name, int? bureauId);
        #endregion

        #region create

        ValidationResult<Employer> CreateEmployer(Employer employer);
        AspNetUserBureau CreateAspNetUserBureau(AspNetUserBureau aspNetUserBureau);
        ValidationResult<Employee> CreateEmployee(Employee employee);
        ValidationResult<Bureau> CreateBureau(Bureau bureau);
        #endregion

        #region edit

        ValidationResult<Employer> UpdateEmployer(Employer employer);
        ValidationResult<Employee> UpdateEmployee(Employee employer);
        ValidationResult<Bureau> UpdateBureau(Bureau bureau);

        #endregion

        #region Update


        #endregion
    }
}
