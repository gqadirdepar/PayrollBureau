using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Data.Models.Ordering;
using PayrollBureau.Data.Models.Paging;

namespace PayrollBureau.Business.Interfaces
{
    public interface IPayrollBureauBusinessService
    {
        Employer RetrieveEmployerByUserId(string id);
        PagedResult<Employer> RetrieveEmployerByBureauId(int id, List<OrderBy> orderBy, Paging paging);
    }
}
