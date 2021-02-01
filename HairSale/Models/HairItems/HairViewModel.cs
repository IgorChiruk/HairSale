using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HairSale.Models.HairItems
{
    [DataContract]
    public class HairViewModel
    {
        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public int Price { get; set; }

        [DataMember]
        [Required]
        public string PostedImageData { get; set; }

        [DataMember]
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct type")]
        public HairType HairType { get; set; }
        //public int SelectedType { get; set; }
        [DataMember]
        public List<HairLength> HairLengths { get; set; }
        [DataMember]
        public List<HairColor> HairColors { get; set; }
       
    }
}