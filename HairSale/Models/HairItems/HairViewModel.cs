using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HairSale.Models.HairItems
{
    public class HairViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct type")]
        public HairType HairType { get; set; }
        [Required]
        public HttpPostedFileBase PostedImage { get; set; }
    }
}