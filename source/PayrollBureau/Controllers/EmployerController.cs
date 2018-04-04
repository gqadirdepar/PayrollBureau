using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;
using PayrollBureau.Models.Authorization;

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
                var result = _PayrollBureauBusinessService.RetrieveEmployer(bureauId, orderBy, paging);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

        [HttpGet]
        [Route("Employer/Create/{bureauId}")]
        public ActionResult Create(int bureauId)
        {
            var userId = User.Identity.GetUserId();
            var bureau = _PayrollBureauBusinessService.RetrieveBureau(bureauId);
            var model = new EmployerViewModel { BureauId = bureau.BureauId , BureauName = bureau.Name};
            return View(model);
        }

        [HttpPost]
        [Route("Employer/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployerViewModel viewModel)
        {
            try
            {
                var validationResult = _PayrollBureauBusinessService.EmployerAlreadyExists(viewModel.Employer.Name,viewModel.Email, null);
                if (!validationResult.Succeeded)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(viewModel);
                }

                //create employeruser and role
                var user = new ApplicationUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                };

                var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Employer").Id;
                user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
                var result = UserManager.Create(user, "Inland12!");
                if (!validationResult.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
                //create employer
                viewModel.Employer.BureauId = viewModel.BureauId;
                viewModel.Employer.AspnetUserId = user.Id;
                var employer = _PayrollBureauBusinessService.CreateEmployer(viewModel.Employer);
                if (employer.Succeeded) return RedirectToAction("Index", "Employer");
              
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index", "Employer");
        }
  
        [Route("Employers/{employerId}/Employees")]
        public ActionResult Employees(int employerId)
        {
            var employer = _PayrollBureauBusinessService.RetrieveEmployer(employerId);
            var model = new BaseViewModel { EmployerId = employerId };
            return View(model);
        }
    }
}