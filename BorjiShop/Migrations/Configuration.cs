using System.Collections.Generic;
using BorjiShop.Models;

namespace BorjiShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BorjiShop.DAL.BorjiShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BorjiShop.DAL.BorjiShopContext context)
        {
            var users = new List<User>
            {
                new User() { UserRole = 1, Email="borjishop@gmail.com", Password = "7d786232b4aaa42be1ab07bfe59e982b", FirstName = "مهدی", LastName = "برجی", Phone = "09355386248", CreateDate = DateTime.Now, IsActive = true, IsDeleted = false}
            };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            var userRoles = new List<UserRole>
            {
                new UserRole() {EnRoleName = "admin", PeRoleName = "مدیر سایت"},
                new UserRole() { EnRoleName = "user", PeRoleName = "کاربر عادی"}
            };
            userRoles.ForEach(u => context.UserRoles.AddOrUpdate(p=>p.EnRoleName, u)) ;
            context.SaveChanges();
        }
    }
}
