using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HairSale.Models.UserAndRoles;

namespace HairSale.Models.Basket
{
    public class UserBasket
    {
        public UserBasket() { }

        public string Id { get; set; }

        public User User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}