using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Runtime.CompilerServices;

namespace HairSale.Models.UserAndRoles
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store)
            : base(store)
        {}             

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                            IOwinContext context)
        {          
            AppContext db = context.Get<AppContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(db));
            return manager;    
        }

    }
}