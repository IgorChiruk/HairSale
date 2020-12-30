using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSale.Models.Orders
{
    public enum OrderStatus
    {
        Complete =1,
        Waiting =2
    }
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public Order(OrderViewModel order)
        {
            OrderItems = new List<OrderItem>();
            this.Date = DateTime.Now;
            this.OrderStatus = OrderStatus.Waiting;
            this.UserEmail = order.UserEmail;
            this.UserPhone = order.UserPhone;
        }

        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}