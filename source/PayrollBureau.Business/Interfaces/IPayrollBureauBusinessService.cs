
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;

namespace PayrollBureau.Business.Interfaces
{
    public interface IPayrollBureauBusinessService
    {

        #region Retrieve

        Statistics Retrievestatistics();
        PagedResult<BureauGrid> RetrieveBureau(string searchTerm, List<OrderBy> orderBy, Paging paging);
        Employer RetrieveEmployerByUserId(string id);
        PagedResult<Employer> RetrieveEmployerByBureauId(int id, List<OrderBy> orderBy, Paging paging);
        PagedResult<Employee> RetrieveEmployees(Expression<Func<Employee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Employee RetrieveEmployee(int employeeId);
        #endregion


    }
}
