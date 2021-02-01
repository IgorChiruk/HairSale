using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HairSale.Models.HairItems;
using HairSale.Models.HairImages;
using System.Data.Entity;

namespace HairSale.Models.HairItems
{
    public class HairItemManager
    {
        readonly AppContext appContext;
        private HairItemManager(AppContext context)             
        {
            this.appContext = context;
        }

        public static HairItemManager Create()
        {
            return new HairItemManager(new AppContext());
        }

        public void Add() 
        { 

        }

        public void Delete()
        {

        }

        public void Update()
        {

        }

        public IEnumerable<HairItem> GetItems()
        {
            return appContext.HairItems.ToList();
        }

        public IEnumerable<HairItem> GetItem(string id)
        {
            return appContext.HairItems.ToList();
        }
    }
}