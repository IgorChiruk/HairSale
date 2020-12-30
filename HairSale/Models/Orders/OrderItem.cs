using HairSale.Models.HairItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSale.Models.Orders
{
    public class OrderItem
    {
        public OrderItem(){}

        public int Id { get; set; }
        public int Quality { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int HairItemId { get; set; }
        public HairItem HairItem { get; set; }
    }
}