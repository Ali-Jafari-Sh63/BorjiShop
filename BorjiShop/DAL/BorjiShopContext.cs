using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Serialization.Json;
using BorjiShop.Models;

namespace BorjiShop.DAL
{
    public class BorjiShopContext : DbContext
    {
        public BorjiShopContext() : base("BorjiShopContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BorjiShopContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BorjiShopContext, BorjiShop.Migrations.Configuration>());
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Piecetype> Piecetypes { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ShowShoppingCart> ShowShoppingCarts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<PieceKey> PieceKeys { get; set; }  

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    base.OnModelCreating(modelBuilder);
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        //}
    }
}