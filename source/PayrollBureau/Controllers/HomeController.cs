using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Extensions;


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
            return View();
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