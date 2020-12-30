using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HairSale.Models.UserAndRoles
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Enter login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}