using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HairSale.Models.HairItems
{
    [DataContract]
    public class HairLength
    {
        public HairLength() { }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Length { get; set; }
    }
}