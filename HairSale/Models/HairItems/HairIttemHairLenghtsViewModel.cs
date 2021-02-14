using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HairSale.Models.HairItems
{
    public class HairIttemHairLenghtsViewModel
    {
        public IEnumerable<HairItem> HairItems { get; set; }
        public IEnumerable<HairLength> HairLengths { get; set; }
    }
}