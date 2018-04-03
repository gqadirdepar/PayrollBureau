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
    public class HomeController : Controller
    {
        private readonly IPayrollBureauBusinessService _payrollBureauBusinessService;

        public HomeController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var model = new HomeViewModel();
            if (User.IsEmployer())
            {
                var data = _payrollBureauBusinessService.RetrieveEmployerByUserId(userId);
                model.EmployerId = data.EmployerId;
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

    }
}