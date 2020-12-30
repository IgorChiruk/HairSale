using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using HairSale.Models.UserAndRoles;
using Microsoft.AspNet.Identity;
using HairSale.Models.Basket;

namespace HairSale.Models.DBInitializer
{
    public class ContextInitializer : CreateDatabaseIfNotExists<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            var roleStore = new RoleStore<Role>(context);
            var roleManager = new AppRoleManager(roleStore);
            roleManager.Create(new Role() { Name = "admin" });
            roleManager.Create(new Role() { Name = "user" });

            var userStore = new UserStore<User>(context);
            var userManager = new AppUserManager(userStore);
            User admin = new User() { UserName = "admin", LastEnter = DateTime.Now, UserBasket = new UserBasket() };

        //TODO
            userManager.Create(admin, "1Admin!");
            userManager.AddToRole(admin.Id, "admin");
        }
    }
}