using HairSale.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using HairSale.Models.Orders;

namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        public ActionResult GetOrderViewMenu()
        {           
                return View("OrderViewMenu");        
        }
        public ActionResult GetAllOrders()
        {
            using (AppContext context = new AppContext())
            {
                var orders = context.Orders.Include(x => x.OrderItems).ToList();
                return View("GetOrders",orders);
            }           
        }

        public ActionResult GetCompleteOrders()
        {
            using (AppContext context = new AppContext())
            {
                var orders = context.Orders.Where(x => x.OrderStatus == OrderStatus.Complete).Include(x => x.OrderItems).ToList();
                return View("GetOrders", orders);
            }
        }

        public ActionResult GetWaitingOrders()
        {
            using (AppContext context = new AppContext())
            {
                var orders = context.Orders.Where(x => x.OrderStatus == OrderStatus.Waiting).Include(x => x.OrderItems).ToList();
                return View("GetOrders",orders);
            }
        }

        public ActionResult GetOrder(int Id)
        {
            using (AppContext context = new AppContext())
            {
                var order = context.Orders.Include(x => x.OrderItems).Where(x=>x.Id==Id).FirstOrDefault();
                foreach (OrderItem item in order.OrderItems)
                {
                    context.Entry(item).Reference("HairItem").Load();
                    context.Entry(item.HairItem).Reference("HairImage").Load();
                }
                return View(order);
            }
        }

        public JsonResult CompleteOrder(int Id)
        {
            return Json("");
        }
    }
}