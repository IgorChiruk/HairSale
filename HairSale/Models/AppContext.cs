using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using HairSale.Models.UserAndRoles;
using HairSale.Models.Basket;
using HairSale.Models.DBInitializer;
using HairSale.Models.HairItems;
using HairSale.Interfaces;
using HairSale.Models.HairImages;
using HairSale.Models.Orders;

namespace HairSale.Models
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext()
            : base("Default")
        { this.Configuration.LazyLoadingEnabled = false; }

        static AppContext()
        {
            Database.SetInitializer(new ContextInitializer());
        }

        public static AppContext Create()
        {
            return new AppContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(s => s.UserBasket).WithRequiredPrincipal(a => a.User).WillCascadeOnDelete(true);
            modelBuilder.Entity<HairItem>().HasRequired(s => s.HairImage).WithRequiredPrincipal(a => a.HairItem).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserBasket> UserBaskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<HairItem> HairItems { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}