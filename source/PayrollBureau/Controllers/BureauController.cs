using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Data.Models;
using PayrollBureau.Extensions;
using PayrollBureau.Models;

namespace PayrollBureau.Controllers
{
    public class BureauController : Controller
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public BureauController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }
        // GET: Bureau

        [Authorize(Roles = "SuperUser")]
        [Route("Bureau")]
        public ActionResult Index()
        {
            return View();
        }

        //    // GET: Bureau/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Bureau/Create
        //    [HttpPost]
        //    public ActionResult Create(FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add insert logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Bureau/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: Bureau/Edit/5
        //    [HttpPost]
        //    public ActionResult Edit(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

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

        [Authorize(Roles = "SuperUser")]
        [HttpPost]
        [Route("Bureau/List")]
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

        [Route("Bureau/{bureauId}")]
        public ActionResult DashBoard(int bureauId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            var model = new BaseViewModel() { BureauId = bureau.BureauId, BureauName = bureau.Name };
            return View(model);
        }

        [Route("Bureau/{bureauId}/Employers")]
        public ActionResult Employers(int bureauId)
        {
            var bureau = _payrollBureauBusinessService.RetrieveBureau(bureauId);
            var model = new BaseViewModel { BureauId = bureau.BureauId, BureauName = bureau.Name };
            return View(model);
        }
    }
}
