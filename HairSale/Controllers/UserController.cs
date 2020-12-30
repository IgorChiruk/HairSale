using HairSale.Models.UserAndRoles;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HairSale.Models;
using System.Xml.Serialization;
using System.Data.Entity;

namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : ApiController
    {
        private readonly Models.AppContext context = Models.AppContext.Create();

        [HttpGet] 
        public int Get()
        {
            var users= context.Users.Count();
            return users;
        }

        [HttpDelete]
        public void DeleteUsers(int id)
        {
            //todo
            DateTime removalThreshold = DateTime.Now.AddDays(-(double)id);
            var users = context.Users.Include(x=>x.UserBasket).Where(x=>x.LastEnter<removalThreshold).ToList();
            var user = users.Where(x => x.UserName == "admin").FirstOrDefault();
            if (user != null)
            {
                users.Remove(user);
            }
            foreach (User _user in users)
            {
                var ccc = _user.UserBasket.Id;             
                context.Users.Remove(_user);
                context.SaveChanges();
            }         
        }
    }
}
