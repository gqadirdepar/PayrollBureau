using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models.Ordering;
using PayrollBureau.Data.Models.Paging;
using PayrollBureau.Extensions;
using PayrollBureau.Models;

namespace PayrollBureau.Controllers
{
    public class EmployerController : BaseController
    {
        public readonly IPayrollBureauBusinessService _PayrollBureauBusinessService;
        // GET: Employer
        public EmployerController(IPayrollBureauBusinessService PayrollBureauBusinessService) : base(PayrollBureauBusinessService)
        {
            _PayrollBureauBusinessService = PayrollBureauBusinessService;
        }
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            //var employer = _PayrollBureauBusinessService.RetrieveEmployerByUserId(userId);
            //var viewModel = new EmployerViewModel
            //{
            //   Employer = employer,
            //};
            //return View(viewModel);
            return View();
        }

        // Employer/List
        [HttpPost]
        [Route("Employer/List")]
        public ActionResult List(int bureauId, Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var result = _PayrollBureauBusinessService.RetrieveEmployerByBureauId(bureauId, orderBy, paging);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

        [Route("Employer/{employerId}/Employees")]
        public ActionResult Employees(int employerId)
        {
            var employer = new EmployerViewModel() { EmployerId = employerId };
            return View(employer);
        }
    }
}