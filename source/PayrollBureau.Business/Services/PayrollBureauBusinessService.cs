﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Data.Interfaces;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Interfaces;
using PayrollBureau.Data.Models.Ordering;
using PayrollBureau.Data.Models.Paging;

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
        #endregion































        public PagedResult<Employee> RetrieveEmployees(Expression<Func<Employee, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _payrollBureauDataService.RetrievePagedResult(predicate, orderBy, paging);
        }

        public Employee RetrieveEmployee(int employeeId)
        {
            return _payrollBureauDataService.Retrieve<Employee>(employeeId);
        }
    }
}