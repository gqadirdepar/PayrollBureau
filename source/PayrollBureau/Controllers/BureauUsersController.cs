using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;
using PayrollBureau.Models.Authorization;

namespace PayrollBureau.Controllers
{
    [RoutePrefix("Bureaus")]
    public class BureauUsersController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public BureauUsersController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }
        // GET: Users
        [Route("{bureauId}/Users")]
        public ActionResult Users(int bureauId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            var model = new BaseViewModel
            {
                BureauId = bureauId,
                BureauName = bureau.Name
            };
            return View(model);

        }

        // GET: Users/Details/5
        [Route("{BureauId}/Users/{userId}")]
        public ActionResult View(int bureauId, string userId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            var user =  UserManager.FindById(userId);
            if (user == null)
                return HttpNotFound();

            var viewModel = new BureauUsersViewModel
            {
                BureauId = bureau.BureauId,
                BureauName = bureau.Name,
                User = new User
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Username = user.UserName,
                    Email = user.Email,
                }
            };

            return View(viewModel);
        }

        // GET: Users/Create
        [Route("{bureauId}/Users/Create")]
        public ActionResult Create(int bureauId)
        {
            var bureau =  _payrollBureauBusinessService.RetrieveBureau(bureauId);
            var viewModel = new BureauUsersViewModel
            {
                BureauId = bureau.BureauId,
                BureauName = bureau.Name,
                User = new User()
                
            };
            return View(viewModel);
        }

        // POST: Users/Create
        [Route("{bureauId}/Users/Create")]
        [HttpPost]
        public ActionResult Create(BureauUsersViewModel model)
        {
            try
            {
                //await ValidateUserExists(clientUser.User);
                //ModelState.RemoveError<ClientUserViewModel>(e => e.User.Role);
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.User.Username,
                        Email = model.User.Email,
                        EmailConfirmed = false,
                         Name = model.User.Name

                    };

                    // Add Client Role
                    var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "Bureau").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    var result =  UserManager.Create(user);
                    if (result.Succeeded)
                    {
                        var newUser =  UserManager.FindByName(user.UserName);
                        var bureauAspNetUser = new AspNetUserBureau
                        {
                            BureauId = model.BureauId,
                            AspNetUserId = newUser.Id

                        };
                         _payrollBureauBusinessService.CreateAspNetUserBureau(bureauAspNetUser);
                        return RedirectToAction("Edit", new { bureauId = model.BureauId, userId = newUser.Id });

                    }

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("CreateUserError", error);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CreateUserError", ex.Message);
            }
            var bureau = _payrollBureauBusinessService.RetrieveBureau(model.BureauId);

            model.BureauId = bureau.BureauId;
            model.BureauName = bureau.Name;
            return View(model);
        }

        // GET: Users/Edit/5
        [Route("{bureauId}/Users/Edit/{userId}")]
        public ActionResult Edit(int bureauId, string userId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);

            var user =  UserManager.FindById(userId);
            if (user == null)
                return HttpNotFound();

            var viewModel = new BureauUsersViewModel
            {
                BureauId = bureau.BureauId,
                BureauName = bureau.Name,
                User = new User
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Username = user.UserName,
                    Email = user.Email,
                }
            };

            return View(viewModel);
        }
        // POST: Users/Edit/5
        [HttpPost]
        [Route("{bureauId}/Users/Edit/{userId}")]
        public ActionResult Edit(BureauUsersViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserManager.FindById(model.User.UserId);
                    user.UserName = model.User.Username;
                    user.Email = model.User.Email;
                    user.Name = model.User.Name;
                    var result =  UserManager.Update(user);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Users", new {bureauId = model.BureauId });
                    }

                    foreach (var error in result.Errors)
                        ModelState.AddModelError("UpdateUserError", error);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EditUserError", ex.Message);
            }

            var bureau = _payrollBureauBusinessService.RetrieveBureau(model.BureauId);

            model.BureauId = bureau.BureauId;
            model.BureauName = bureau.Name;
            return View(model);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [Route("BureauUsers/List")]
        public ActionResult List(int bureauId,string searchTerm, Paging paging, List<OrderBy> orderBy)
        {
            try
            {
                var result = _payrollBureauBusinessService.RetrieveBureauUsers(bureauId,searchTerm,orderBy, paging);
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

    }
}
