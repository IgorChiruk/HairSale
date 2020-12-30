using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HairSale.Models.HairItems;
using HairSale.Models.HairImages;
using System.Data.Entity;

namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class HairsController : ApiController
    {
        private Models.AppContext context = Models.AppContext.Create();
        // GET: api/Hairs
        public IEnumerable<HairItem> Get()
        {
            return context.HairItems.Include(x => x.HairImage).ToList();
        }

        // GET: api/Hairs/5
        public HairItem Get(string id)
        {
            return context.HairItems.Find(id);
        }

        public void Post()
        {
            var httpRequest = HttpContext.Current.Request;
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                byte[] bytes;

                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }

                var name = httpRequest.Form["name"];
                var price = httpRequest.Form["price"];
                var pr = Convert.ToInt32(price);

                ImageEntity image = new ImageEntity() { Name = postedFile.FileName, ContentType = postedFile.ContentType, Data = bytes };
                HairItem hair = new HairItem() { Name = name, Price = Convert.ToInt32(price), HairImage = image };
                context.Images.Add(image);
                context.HairItems.Add(hair);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var item = context.HairItems.Find(id);
            if (item != null)
            {
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }         
        }
    }
}
