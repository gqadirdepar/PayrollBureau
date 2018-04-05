using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;

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

        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees/{employeeId}")]
        public ActionResult DashBoard(int employeeId)
        {
            var employee = _payrollBureauBusinessService.RetrieveEmployee(employeeId);
            var model = new BaseViewModel
            {
                EmployerId = employee.EmployerId,
                BureauId = employee.Employer.BureauId,
                BureauName = employee.Employer.Bureau.Name,
                EmployerName = employee.Employer.Name,
                EmployeeName = employee.Name,
                EmployeeId = employee.EmployeeId
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Documents(int employeeId, Paging paging, List<OrderBy> orderBy)
        {
            var data = _payrollBureauBusinessService.RetrieveEmployeeDocuments(e => e.EmployeeId == employeeId, orderBy, paging);
            return this.JsonNet(data);
        }
    }
}