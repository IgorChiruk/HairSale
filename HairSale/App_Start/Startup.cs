using Microsoft.Owin;
using Owin;
using HairSale.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using HairSale.Models.UserAndRoles;

[assembly: OwinStartup(typeof(HairSale.App_Start.Startup))]

namespace HairSale.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {       
            app.CreatePerOwinContext(AppContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName="HS",               
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),               
            });
        }
    }
}
