//using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HairSale.Models.HairItems;
using HairSale.Models.HairImages;
using System.Data.Entity;
using System.IO;
using HairSale.Models;

namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class HairController : Controller
    {       
        public ActionResult GetHairs()
        {
            using (AppContext context = AppContext.Create())
            {
                var hairs = context.HairItems.Include(x => x.HairImage).ToList();
                return PartialView(hairs);
            }
        }

        public JsonResult DeleteHair(int id)
        {
            using (AppContext context = AppContext.Create())
            {
                var item = context.HairItems.Find(id);
                if (item != null)
                {
                    context.Entry(item).State = EntityState.Deleted;
                    context.SaveChanges();
                    return Json(true);
                }
                return Json(false);
            }
            
        }

        [HttpPost]
        public JsonResult AddHair(string name, string price, HttpPostedFileBase postedFile)
        {
            using (AppContext context = AppContext.Create())
            {
                if (name != null && price != null && postedFile != null)
                {
                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                    {
                        bytes = br.ReadBytes(postedFile.ContentLength);
                    }

                    ImageEntity image = new ImageEntity() { Name = postedFile.FileName, ContentType = postedFile.ContentType, Data = bytes };
                    HairItem hair = new HairItem() { Name = name, Price = System.Convert.ToInt32(price), HairImage = image };
                    context.Images.Add(image);
                    context.HairItems.Add(hair);
                    context.SaveChanges();

                    return Json(true);
                }
                return Json(false);
            }      
        }
    }
}