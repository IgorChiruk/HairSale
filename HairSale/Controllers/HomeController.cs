using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using HairSale.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using HairSale.Models.HairItems;
using HairSale.Models.Orders;
using System.Data.Entity;
using HairSale.Models.Basket;

namespace HairSale.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult CatalogGood()
        {//todo
            using (AppContext context = new AppContext())
            {
                HairIttemHairLenghtsViewModel model = new HairIttemHairLenghtsViewModel();
                HairItemManager manager = HairItemManager.Create();
                model.HairItems = manager.GetItems();
                model.HairLengths = context.HairLengths.ToList();
                return View(model);
            }
        }

        public ActionResult BuyingHair()
        {
            return View();
        }

        public ActionResult PaymentAndDelivery()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult QuestionAndAnswers()
        {
            return View();
        }

        public ActionResult HowToBuy()
        {
            return View();
        }

        public ActionResult WhyWe()
        {
            return View();
        }

        public ActionResult Refund()
        {
            return View();
        }

        public ActionResult SiteMap()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Ordering()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ordering(OrderViewModel orderView)
        {
            using (AppContext context = new AppContext())
            {
                Order order = new Order(orderView);
                var userId = context.Users.Where(x => x.UserName == this.User.Identity.Name).FirstOrDefault().Id;
                var basketItems = context.BasketItems.Include(x => x.HairItem).Where(x => x.UserBasketId == userId).ToList();
                foreach (BasketItem item in basketItems)
                {
                    OrderItem orderItem = new OrderItem() { HairItem = item.HairItem, Order = order, Quality = item.Quality };
                    order.OrderItems.Add(orderItem);
                }
                context.Orders.Add(order);
                context.SaveChanges();
                return View();
            }
        }
    }
}