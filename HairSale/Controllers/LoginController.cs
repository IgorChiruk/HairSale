using System;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.Owin.Security;
using HairSale.Models.UserAndRoles;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using HairSale.Models.Basket;

namespace HairSale.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            //todo проврка на нахождение пользователя в бд
            User user = new User() { UserBasket = new UserBasket() };
            user.UserName = this.Session.SessionID;
            user.LastEnter = DateTime.Now;
            IdentityResult result = await UserManager.CreateAsync(user);
            ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);

            var returnUrl = Request.QueryString["ReturnUrl"];

            if (returnUrl != null && !returnUrl.ToLower().Contains("admin")) { return Redirect(returnUrl); }
            else { return Redirect("/Home/Index"); }
        }

        [AllowAnonymous]
        public async Task RefreshCookie()
        {
            AuthenticationManager.SignOut();
            User user = new User() { UserBasket = new UserBasket() };
            user.UserName = this.Session.SessionID;
            user.LastEnter = DateTime.Now;
            IdentityResult result = await UserManager.CreateAsync(user);
            ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
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