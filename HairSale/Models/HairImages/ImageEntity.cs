using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using HairSale.Models.HairItems;

namespace HairSale.Models.HairImages
{
    [DataContract]
    public class ImageEntity
    {     
        public ImageEntity()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        [DataMember]
        public byte[] Data { get; set; }
        [IgnoreDataMember]
        public HairItem HairItem { get; set; }
    }
}