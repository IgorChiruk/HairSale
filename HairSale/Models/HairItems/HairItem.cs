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
    public enum HairType
    {
        Slavian = 1,
        Russian = 2
    }

    public class HairItem
    {
        public HairItem(){}

        public int Id { get; set; }

        public string Name { get; set; }
      
        public int Price { get; set; }

        public ImageEntity HairImage { get; set; }
        public HairType HairType { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<HairColor> HairColors { get; set; }
        public ICollection<HairLength> HairLengths { get; set; }
    }
}