using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;
using PayrollBureau.Models.Authorization;

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

        [HttpGet]
        [Route("Employee/Create/{employerId}")]
        public ActionResult Create(int employerId)
        {
            var employer = _payrollBureauBusinessService.RetrieveEmployer(employerId);
            var model = new EmployeeViewModel { EmployerId = employer.EmployerId, EmployerName = employer.Name };
            return View(model);
        }

        [HttpPost]
        [Route("Employee/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            try
            {
                var validationResult = _payrollBureauBusinessService.EmployeeAlreadyExists(viewModel.Employee.Name, null);
                if (!validationResult.Succeeded)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(viewModel);
                }

                //create employee user and role
                var user = new ApplicationUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                };

                var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Employee").Id;
                user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
                var result = UserManager.Create(user, "Inland12!");
                if (!validationResult.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
                //create employee
                var userId = User.Identity.GetUserId();
                viewModel.Employee.EmployerId = viewModel.EmployerId;
                viewModel.Employee.AspnetUserId = user.Id;
                viewModel.Employee.CreatedBy = userId;
                viewModel.Employee.CreatedDateUtc = DateTime.UtcNow;
                var employee = _payrollBureauBusinessService.CreateEmployee(viewModel.Employee);
                if (employee.Succeeded) return RedirectToAction("Employees", "Employer", new { employerId = viewModel.EmployerId });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Employees", "Employer", new { employerId = viewModel.EmployerId });
        }


        [HttpGet]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employee/{employeeId}/Edit")]
        public ActionResult Edit(int employeeId)
        {

            var employee = _payrollBureauBusinessService.RetrieveEmployee(employeeId);
            if (employee == null)
                return RedirectToAction("NotFound", "Error");

            var employer = _payrollBureauBusinessService.RetrieveEmployer(employee.EmployerId);
            var model = new EmployeeViewModel { EmployerId = employer.EmployerId, EmployerName = employer.Name, Employee = employee };
            return View(model);
        }

        [HttpPost]
        [Route("Employee/Edit")]
        public ActionResult Edit(EmployeeViewModel model)
        {

            var result = _payrollBureauBusinessService.UpdateEmployee(model.Employee);
            if (result.Succeeded) return RedirectToAction("Employees", "Employer",new { employerId = model.EmployerId});
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

    }
}