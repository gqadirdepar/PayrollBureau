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
        PagedResult<Employee> RetrieveEmployees(Expression<Func<Employee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Employee RetrieveEmployee(int employeeId);
        Bureau RetrieveBureau(int bureauId);
        Bureau RetrieveBureau(string aspNetUserId);

        #endregion


    }
}
