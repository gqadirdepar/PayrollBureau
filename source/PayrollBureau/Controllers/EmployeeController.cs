using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Common.Enum.Document;
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

        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees")]
        public ActionResult Index(int? bureauId, int? employerId)
        {
            var employer = _payrollBureauBusinessService.RetrieveEmployer(employerId.Value);
            var model = new BaseViewModel
            {
                BureauId = employer.BureauId,
                BureauName = employer.Bureau.Name,
                EmployerId = employer.EmployerId,
                EmployerName = employer.Name
            };
            return View(model);
        }

        [HttpPost]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees/List")]
        public ActionResult List(int bureauId, int employerId, Paging paging, List<OrderBy> orderBy)
        {
            var data = _payrollBureauBusinessService.RetrieveEmployees(e => e.BureauId == bureauId && e.EmployerId == employerId, orderBy, paging);
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
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees/{employeeId}/Documents")]
        public ActionResult Documents(int bureauId, int employerId, int employeeId, Paging paging, List<OrderBy> orderBy)
        {
            var documents = _payrollBureauBusinessService.RetrieveEmployeeDocuments(e => e.BureauId == bureauId && e.EmployeeId == employeeId && e.DocumentCategoryId == (int)DocumentCategory.Document, orderBy, paging);
            return this.JsonNet(documents);
        }

        [HttpPost]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees/{employeeId}/Payslips")]
        public ActionResult Payslips(int bureauId, int employerId, int employeeId, Paging paging, List<OrderBy> orderBy)
        {
            var payslips = _payrollBureauBusinessService.RetrieveEmployeeDocuments(e => e.BureauId == bureauId && e.EmployeeId == employeeId && e.DocumentCategoryId == (int)DocumentCategory.Payslip, orderBy, paging);
            return this.JsonNet(payslips);
        }
    }
}