using HairSale.Models.HairItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSale.Models.Basket
{
    public class BasketItem
    {
        public BasketItem() {}      

        public int Id { get; set; }
        public int Quality { get; set; }
        public int HairItemId { get; set; }
        public HairItem HairItem { get; set; }
        public string UserBasketId { get; set; }
        public UserBasket UserBasket { get; set; }
    }
}