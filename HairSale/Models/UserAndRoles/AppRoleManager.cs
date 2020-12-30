using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HairSale.Models.UserAndRoles
{
    public class AppRoleManager : RoleManager<Role>
    {
        public AppRoleManager(RoleStore<Role> store)
                : base(store)
        { }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options,
                                                IOwinContext context)
        {
            return new AppRoleManager(new
                    RoleStore<Role>(context.Get<AppContext>()));
        }
    }
}