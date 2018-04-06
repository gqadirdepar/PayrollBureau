using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Extensions;
using PayrollBureau.Models;
namespace PayrollBureau.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public HomeController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var model = new BaseViewModel();
            if (User.IsEmployer())
            {
                var data = _payrollBureauBusinessService.RetrieveEmployer(userId);
                model.EmployerId = data.EmployerId;
                model.EmployerName = data.Name;
            }
            if (User.IsBureau())
            {
                var data = _payrollBureauBusinessService.RetrieveBureau(userId);
                model.BureauId = data.BureauId;
                model.BureauName = data.Name;
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Extension
        [HttpPost]
        [Route("Home/Statistics")]
        public ActionResult Statistics()
        {
            try
            {
                var result = _payrollBureauBusinessService.Retrievestatistics();
                return this.JsonNet(result);
            }
            catch (Exception ex)
            {
                return this.JsonNet(ex);
            }
        }

    }
}