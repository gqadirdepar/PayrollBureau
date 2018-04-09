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
    public class BureauController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public BureauController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }
        // GET: Bureau

        [Route("Bureaus")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Bureau/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Bureaus/{bureauId}/Employers/{employerId}/Employees")]
        [Route("Bureau/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BureauViewModel viewModel)
        {
            try
            {             
                //create Bureau
                var userId = User.Identity.GetUserId();
                viewModel.Bureau.CreatedBy = userId;
                var bureau = _payrollBureauBusinessService.CreateBureau(viewModel.Bureau);
                if (!bureau.Succeeded)
                {
                    foreach (var error in bureau.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return View(viewModel);
                }

                //create Bureau user and role
                var user = new ApplicationUser
                {
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                };

                var roleId = RoleManager.Roles.FirstOrDefault(r => r.Name == "Bureau").Id;
                user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });
                var result = UserManager.Create(user, "Inland12!");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }

                //create AspNetUser Bureau              
                _payrollBureauBusinessService.CreateAspNetUserBureau(bureau.Entity.BureauId, user.Id);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index", "Bureau");
        }

        [HttpGet]
        [Route("Bureaus/{bureauId}/Edit")]
        public ActionResult Edit(int bureauId)
        {

            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            if (bureau == null)
                return RedirectToAction("NotFound", "Error");
            var model = new BureauViewModel { Bureau = bureau };
            return View(model);
        }

        [HttpPost]
        [Route("Bureaus/Edit")]
        public ActionResult Edit(BureauViewModel model)
        {
            var result = _payrollBureauBusinessService.UpdateBureau(model.Bureau);
            if (result.Succeeded) return RedirectToAction("Index", "Bureau");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }
        //    // GET: Bureau/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Bureau/Delete/5
        //    [HttpPost]
        //    public ActionResult Delete(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        [HttpPost]
        [Route("Bureaus/List")]
        public ActionResult List(string searchTerm, Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var result = _payrollBureauBusinessService.RetrieveBureau(searchTerm, orderBy, paging);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

        [Route("Bureaus/{bureauId}")]
        public ActionResult DashBoard(int bureauId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            return View(RetrieveModel(bureau));
        }

        private BaseViewModel RetrieveModel(Bureau bureau)
        {
            var model = new BaseViewModel
            {
                BureauId = bureau.BureauId,
                BureauName = bureau.Name,
            };
            return model;
        }

        [HttpPost]
        [Route("Bureau/Statistics")]
        public ActionResult Statistics(int bureauId)
        {
            var result = _payrollBureauBusinessService.RetrieveBureauStatistics(bureauId);
            return this.JsonNet(result);
        }
    }
}
