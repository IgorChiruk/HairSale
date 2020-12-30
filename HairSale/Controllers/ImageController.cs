using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HairSale.Models.HairImages;

namespace HairSale.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImageController : ApiController
    {
        private ImageManager manager = new ImageManager();
        // GET: api/Image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Image/5
        public string Get(int id)
        {
            string resourceImageBase64 = string.Empty;
            var image = manager.Get(id);
            resourceImageBase64 = "data:text/plain;base64," + System.Convert.ToBase64String(image.Data);
            return resourceImageBase64;
        }

        // POST: api/Image
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {
        }
    }
}
