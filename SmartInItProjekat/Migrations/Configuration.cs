namespace SmartInItProjekat.Migrations
{
    using GameStore.Domain.Helper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SmartInItProjekat.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartInItProjekat.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartInItProjekat.Models.ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(ApplicationDbContext context)
        {

            GetRoles().ForEach(c => context.Roles.Add(c));
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetFurnitureSalons().ForEach(c => context.FurnitureSalons.Add(c));
            GetBills().ForEach(c => context.Bills.Add(c));
            GetBillItems().ForEach(c => context.BillItems.Add(c));

            context.SaveChanges();
            Hasher hasher = new Hasher();
            var user = new ApplicationUser
            {
                UserName = "marko",
                PasswordHash = hasher.HashPassword("admin"),
                Email = "bobic015@gmail.com",
                FirstName = "Marko",
                LastName = "Bobic",
                Adress = "Milutina Bojica 15",
                UserRoles = "Admin"
            };
            var role = context.Roles.Where(r => r.Name == "Admin").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            var user2 = new ApplicationUser
            {
                UserName = "goran",
                PasswordHash = hasher.HashPassword("buyer"),
                Email = "goran@gmail.com",
                FirstName = "Goran",
                LastName = "Maksimovic",
                Adress = "Milutina Bojica 15",
                UserRoles = "Buyer"
            };
            var role2 = context.Roles.Where(r => r.Name == "Buyer").First();
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = user.Id });
            context.Users.Add(user);
            context.SaveChanges();
        }

        private static List<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole> {
               new IdentityRole {Name=RoleName.Admin},
               new IdentityRole {Name=RoleName.Buyer}
            };

            return roles;
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
               new Category {CategoryId=1, Name="Sofa",Description="Elegant sofa" },
               new Category {CategoryId=2, Name="Chair",Description="Elegant chair"},
               new Category {CategoryId=3, Name="Bed",Description="Elegant bed."}
            };

            return categories;
        }

        private static List<FurnitureSalon> GetFurnitureSalons()
        {
            var furnituresSalon = new List<FurnitureSalon>()
            {
                new FurnitureSalon { AccountNumber="123125325235235324", Adress="Goran Bikovic br:32", Email="ggsp@gmail.com", Name="GGSP", FurnitureSalonId=1,
                    Owner="Jovan Jovanovic", PIB="131254124412", WebPage="www.ssnsp.com", TelephoneNumber="021-222-22-11"}
            };

            return furnituresSalon;
        }

        private static List<Bill> GetBills()
        {
            var bills = new List<Bill>()
            {
                new Bill{ BillId=1, Buyer="Zoran Zoranovic", Tax=0.20M, TimeOfPurchase= DateTime.Now, TotalPrice=8000M}
            };
            return bills;
        }
        private static List<BillItem> GetBillItems()
        {
            var billItems = new List<BillItem>()
            {
                new BillItem { Amount=4, BillId=1, BillItemId=1, Furniture="King bed" , FurnitureCategory="Bed", FurnitureShop="Simpa", Price=8000M }
            };
            return billItems;
        }
    }
}
