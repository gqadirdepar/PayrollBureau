using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;

namespace PayrollBureau.Controllers
{
    public class EmployerController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;
        // GET: Employer
        public EmployerController(IPayrollBureauBusinessService payrollBureauBusinessService) : base(payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("Bureaus/{bureauId}/Employers")]
        public ActionResult Index(int? bureauId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId.Value);
            var model = new BaseViewModel
            {
                BureauId = bureau.BureauId,
                BureauName = bureau.Name,
            };
            return View(model);
        }

        // Employer/List
        [HttpPost]
        [Route("Bureaus/{bureauId}/Employer/List")]
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

        [Route("Bureaus/{bureauId}/Employers/{employerId}")]
        public ActionResult DashBoard(int employerId)
        {
            var employer = _payrollBureauBusinessService.RetrieveEmployer(employerId);
            return View(RetrieveModel(employer));
        }

        private BaseViewModel RetrieveModel(Employer employer)
        {
            var model = new BaseViewModel
            {
                EmployerId = employer.EmployerId,
                BureauId = employer.BureauId,
                BureauName = employer.Bureau.Name,
                EmployerName = employer.Name
            };
            return model;
        }
    }
}