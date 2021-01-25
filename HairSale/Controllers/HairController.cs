﻿//using System;
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

        public ActionResult GetHairLength()
        {
            using (AppContext context = AppContext.Create())
            {
                var length = context.HairLengths.OrderBy(x => x.Length).ToList();
                return PartialView(length);
            }
        }

        public ActionResult GetHairColor()
        {
            using (AppContext context = AppContext.Create())
            {
                var colors = context.HairColors.ToList();
                return PartialView(colors);
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

        [HttpGet]
        public ActionResult AddHair()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AddHair(HairViewModel model)
        {
            using (AppContext context = AppContext.Create())
            {
                if (!ModelState.IsValid)
                {              
                    return Json(false);
                }             
                    byte[] bytes;

                    using (BinaryReader br = new BinaryReader(model.PostedImage.InputStream))
                    {
                        bytes = br.ReadBytes(model.PostedImage.ContentLength);
                    }

                    ImageEntity image = new ImageEntity() { Name = model.PostedImage.FileName, ContentType = model.PostedImage.ContentType, Data = bytes };
                    HairItem hair = new HairItem() { Name =model.Name, Price = model.Price, HairImage = image };
                    context.Images.Add(image);
                    context.HairItems.Add(hair);
                    context.SaveChanges();
                    return Json(true);              
            }      
        }

        [HttpGet]
        public ActionResult AddHairLenght()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AddHairLenght(HairLength model)
        {
            using (AppContext context = AppContext.Create())
            {
                context.HairLengths.Add(model);
                context.SaveChanges();
                return Json(true);
            }
        }

        [HttpGet]
        public ActionResult AddHairColor()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult AddHairColor(HairColor model)
        {
            using (AppContext context = AppContext.Create())
            {
                context.HairColors.Add(model);
                context.SaveChanges();
                return Json(true);            
            }
        }

        public JsonResult DeleteHairLength(int id)
        {
            using (AppContext context = AppContext.Create())
            {
                var item = context.HairLengths.Find(id);
                if (item != null)
                {
                    context.Entry(item).State = EntityState.Deleted;
                    context.SaveChanges();
                    return Json(true);
                }
                return Json(false);
            }
        }

        public JsonResult DeleteHairColor(int id)
        {
            using (AppContext context = AppContext.Create())
            {
                var item = context.HairColors.Find(id);
                if (item != null)
                {
                    context.Entry(item).State = EntityState.Deleted;
                    context.SaveChanges();
                    return Json(true);
                }
                return Json(false);
            }

        }


        //<div class="modal-content">
        //        ...
        //    </div>
    }
}