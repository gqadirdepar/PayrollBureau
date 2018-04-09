using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PayrollBureau.Business.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Business.Models;
using PayrollBureau.Common.Enum.Document;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;
using PayrollBureau.Models.Authorization;

namespace PayrollBureau.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;
        private readonly IDocumentBusinessService _documentBusinessService;

        public EmployeeController(IPayrollBureauBusinessService payrollBureauBusinessService, IDocumentBusinessService documentBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
            _documentBusinessService = documentBusinessService;
        }


        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees")]
        [Route("Employee/Index/{employerId}")]
        public ActionResult Index(int? employerId)
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

        [HttpPost]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees/{employeeId}/{description}/UploadDocument")]
        public ActionResult UploadDocument(int bureauId, int employerId, int employeeId, string description)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                var employee = _payrollBureauBusinessService.RetrieveEmployee(employeeId);
                var documentMeta = new DocumentMeta
                {
                    EmployeeName = employee.Name,
                    PayrollId = employeeId.ToString(),
                    DocumentTypeId = (int)DocumentCategory.Document,
                    FileName = file.FileName.Split('\\').Last().FilterSpecialChars(),
                    CreatedBy = this.User.Identity.GetUserId(),
                    UploadedDate = DateTime.UtcNow,
                    Description = description
                };
                var result = _documentBusinessService.CreateEmployeeDocument(documentMeta, employeeId, User.Identity.GetUserId());
                if (result.Succeeded)
                    return this.JsonNet(new { Success = true });

            }
            return this.JsonNet(new { Success = false, Error = "Error uploading fit note file." });
        }

        [HttpGet]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Create")]
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
                viewModel.Employee.EmployerId = viewModel.EmployerId.Value;
                viewModel.Employee.AspnetUserId = user.Id;
                viewModel.Employee.CreatedBy = userId;
                var employee = _payrollBureauBusinessService.CreateEmployee(viewModel.Employee);
                if (employee.Succeeded) return RedirectToAction("Index", "Employee", new { employerId = viewModel.EmployerId, });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index", "Employee", new { employerId = viewModel.EmployerId });
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
            if (result.Succeeded) return RedirectToAction("Index", "Employee", new { employerId = model.EmployerId, });
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

    }
}