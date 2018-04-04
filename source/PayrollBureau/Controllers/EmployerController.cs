using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;

namespace PayrollBureau.Controllers
{
    public class EmployerController : BaseController
    {
        public readonly IPayrollBureauBusinessService _payrollBureauBusinessService;
        // GET: Employer
        public EmployerController(IPayrollBureauBusinessService PayrollBureauBusinessService) : base(PayrollBureauBusinessService)
        {
            _payrollBureauBusinessService = PayrollBureauBusinessService;
        }

        public ActionResult Index()
        {
            return View();
        }

        // Employer/List
        [HttpPost]
        [Route("Employer/List")]
        public ActionResult List(int bureauId, Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var result = _payrollBureauBusinessService.RetrieveEmployer(bureauId, orderBy, paging);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees")]
        public ActionResult Employees(int employerId)
        {
            var employer = _payrollBureauBusinessService.RetrieveEmployer(employerId);
            var model = new BaseViewModel
            {
                EmployerId = employerId,
                BureauId = employer.BureauId,
                BureauName = employer.Bureau.Name,
                EmployerName = employer.Name
            };
            return View(model);
        }

        [Route("Bureaus/{bureauId}/Employers/{employerId}")]
        public ActionResult DashBoard(int employerId)
        {
            var employer = _payrollBureauBusinessService.RetrieveEmployer(employerId);
            var model = new BaseViewModel
            {
                EmployerId = employerId,
                BureauId = employer.BureauId,
                BureauName = employer.Bureau.Name,
                EmployerName = employer.Name
            };
            return View(model);
        }
    }
}