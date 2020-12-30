using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using HairSale.Models.Basket;
using HairSale.Models.HairImages;
using HairSale.Models.Orders;

namespace HairSale.Models.HairItems
{
    [DataContract]
    public class HairItem
    {
        public HairItem(){}

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public ImageEntity HairImage { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}