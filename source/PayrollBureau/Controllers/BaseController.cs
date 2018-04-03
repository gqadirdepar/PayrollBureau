using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PayrollBureau.Business.Interfaces;
using PayrollBureau.Models;
using PayrollBureau.Models.Authorization;

namespace PayrollBureau.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUser _applicationUser;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private IPayrollBureauBusinessService _payrollBureauBusinessService;


        protected IPayrollBureauBusinessService PayrollBureauBusinessService
        {
            get
            {
                return _payrollBureauBusinessService;
            }
        }

        public BaseController()
        {
            
        }
        public BaseController(IPayrollBureauBusinessService payrollBureauBusinessService)
        {
            _payrollBureauBusinessService = payrollBureauBusinessService;
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser ApplicationUser
        {
            get
            {
                return _applicationUser ?? UserManager.FindById(User?.Identity?.GetUserId());
            }
            set
            {
                _applicationUser = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        
        //protected TenantOrganisation Organisation => UserManager.TenantOrganisation;


        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_payrollBureauBusinessService != null)
                    _payrollBureauBusinessService = null;

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }

                if (_applicationUser != null)
                    _applicationUser = null;
            }

            base.Dispose(disposing);
        }

        protected ActionResult NotFoundError()
        {
            return RedirectToAction("Index", "Error");
        }
        public ActionResult HttpForbidden()
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }
    }
}