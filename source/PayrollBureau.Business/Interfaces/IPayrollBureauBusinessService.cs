using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models.Ordering;
using PayrollBureau.Data.Models.Paging;

namespace PayrollBureau.Business.Interfaces
{
    public interface IPayrollBureauBusinessService
    {










        PagedResult<Employee> RetrieveEmployees(Expression<Func<Employee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Employee RetrieveEmployee(int employeeId);
    }
}
