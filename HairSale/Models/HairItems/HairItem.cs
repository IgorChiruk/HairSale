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
        public HairItem()
        {
            BasketItems = new List<BasketItem>(); ;
            OrderItems = new List<OrderItem>(); ;
            HairColors = new List<HairColor>(); ;
            HairLengths = new List<HairLength>(); ;
        }

        public int Id { get; set; }

        public string Name { get; set; }
      
        public int Price { get; set; }

        public string HairImage { get; set; }
        public HairType HairType { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<HairColor> HairColors { get; set; }
        public ICollection<HairLength> HairLengths { get; set; }
    }
}