using System.Collections.Generic;
using PayrollBureau.Business.Models;
using  PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;

namespace PayrollBureau.Business.Interfaces
{
    public interface IPayrollBureauBusinessService
    {
        #region Retrieve

        Statistics Retrievestatistics();
        PagedResult<BureauGrid> RetrieveBureau(string searchTerm, List<OrderBy> orderBy, Paging paging);

        #endregion
    }
}
