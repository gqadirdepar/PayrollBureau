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
        public PagedResult<BureauGrid> RetrieveBureau(string searchTerm, List<OrderBy> orderBy, Paging paging)
        {
            return _payrollBureauDataService.RetrieveBureau(searchTerm, orderBy, paging);
            }

      
        public Employer RetrieveEmployerByUserId(string userId)
        {
            return _payrollBureauDataService.RetrieveEmployerByUserId(userId);
        }

        public PagedResult<Employer> RetrieveEmployerByBureauId(int id, List<OrderBy> orderBy, Paging paging)
        {
            if (paging == null)
                return _payrollBureauDataService.RetrievePagedResult<Employer>(t => t.BureauId == id);
            return _payrollBureauDataService.RetrievePagedResult<Employer>(t => t.BureauId == id, orderBy, paging);
        }
      


        public PagedResult<Employee> RetrieveEmployees(Expression<Func<Employee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _payrollBureauDataService.RetrievePagedResult(predicate, orderBy, paging);
        }

        public Employee RetrieveEmployee(int employeeId)
        {
            return _payrollBureauDataService.Retrieve<Employee>(employeeId);

        }
          #endregion
    }
}
