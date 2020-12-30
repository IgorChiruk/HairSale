using HairSale.Models.UserAndRoles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {    
        [AllowAnonymous]
        [HttpGet]       
        public ActionResult Login()
        {           
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Login,model.Password);
                ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                return RedirectToAction("AdminPage","Admin");
            }         
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}