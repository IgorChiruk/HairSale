using System;
using Microsoft.AspNet.Identity.EntityFramework;
using HairSale.Models.Basket;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace HairSale.Models.UserAndRoles
{
    [DataContract]
    public class User : IdentityUser
    {
        public User() { }

        [DataMember]
        public DateTime LastEnter { get; set; }

        [Key]
        [DataMember]
        public override string Id { get => base.Id; set => base.Id = value; }

        [DataMember]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public UserBasket UserBasket { get; set; }
    }
}