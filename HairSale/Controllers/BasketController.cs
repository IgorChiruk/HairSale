using HairSale.Models.Basket;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HairSale.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        public async Task<ActionResult> GetBasket()
        {
            using (Models.AppContext context = Models.AppContext.Create())
            {
                var user = await context.Users.Include(x => x.UserBasket).Where(y => y.UserName == this.User.Identity.Name).FirstOrDefaultAsync();
                if (user != null)
                {
                    context.Entry(user.UserBasket).Collection("BasketItems").Load();
                    foreach (BasketItem item in user.UserBasket.BasketItems)
                    {
                        context.Entry(item).Reference("HairItem").Load();
                        context.Entry(item.HairItem).Reference("HairImage").Load();
                    }

                    if (user.UserBasket.BasketItems.Count <= 0)
                    {
                        return PartialView("GetEmptyBasket");
                    }
                    return PartialView(user.UserBasket);
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<JsonResult> GetBasketTotalCountAndPrice()
        {
            using (Models.AppContext context = Models.AppContext.Create())
            {
                int totalPrice = 0;
                int totalCount = 0;

                var user = await context.Users.Include(x => x.UserBasket).Where(y => y.UserName == this.User.Identity.Name).FirstOrDefaultAsync();
                if (user != null)
                {
                    context.Entry(user.UserBasket).Collection("BasketItems").Load();

                    foreach (BasketItem item in user.UserBasket.BasketItems)
                    {
                        context.Entry(item).Reference("HairItem").Load();
                        totalPrice += item.HairItem.Price * item.Quality;
                        totalCount += item.Quality;
                    }              
                }

                Dictionary<string, string> data = new Dictionary<string, string>(2);
                data.Add("price", totalPrice.ToString());
                data.Add("count", totalCount.ToString());

                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddBasketItem(int itemId)
        {
            using (Models.AppContext context = Models.AppContext.Create())
            {
                var user = context.Users.Where(x => x.UserName == this.User.Identity.Name).FirstOrDefault();
                var item = context.HairItems.Find(itemId);
                if (user != null && item != null)
                {
                    context.Entry(user).Reference("UserBasket").Load();
                    context.Entry(user.UserBasket).Collection("BasketItems").Load();

                    foreach (BasketItem tempItem in user.UserBasket.BasketItems)
                    {
                        context.Entry(tempItem).Reference("HairItem").Load();
                        if (tempItem.HairItem.Id == itemId)
                        {
                            tempItem.Quality += 1;
                            context.SaveChanges();
                            return Json(true);
                        }
                    }

                    BasketItem basketItem = new BasketItem { HairItem = item, Quality = 1, UserBasket = user.UserBasket };
                    context.BasketItems.Add(basketItem);
                    context.SaveChanges();
                    return Json(true);
                }
                return Json(false);
            }
        }

        [HttpDelete]
        public JsonResult DeleteBasketItem(string basketID, int? itemId)
        {
            using (Models.AppContext context = Models.AppContext.Create())
            {
                var basket = context.UserBaskets.Find(basketID);
                if (basket != null && itemId != null)
                {
                    context.Entry(basket).Collection("BasketItems").Load();
                    var item = basket.BasketItems.Where(x => x.Id == itemId).FirstOrDefault();
                    if (item != null)
                    {
                        basket.BasketItems.Remove(item);
                        context.SaveChanges();
                        return Json(true);
                    }
                }
                return Json(false);
            }
        }

        public void ClearBasket()
        {

        }

    }
}