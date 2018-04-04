using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;

namespace PayrollBureau.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public EmployeeController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(int employerId, Paging paging, List<OrderBy> orderBy)
        {
            var data = _payrollBureauBusinessService.RetrieveEmployees(e => e.EmployerId == employerId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}