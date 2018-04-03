using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Interfaces;
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
    }
}
