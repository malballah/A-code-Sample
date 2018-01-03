using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Truck.Models;
using Truck.ViewModels;

namespace Truck.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Truck.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Truck.Models.ApplicationDbContext";
        }

        protected override void Seed(Truck.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            if (!context.Users.Any())
            {
                UserManager<ApplicationUser> userManager =
                    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
               
                userManager.Create(
                    new ApplicationUser
                    {
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                        FirstName = "admin",
                        LastName = "admin"
                    }, "admin@123");

           
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                roleManager.Create(new IdentityRole
                {
                    Name="admin"
                });
                roleManager.Create(new IdentityRole
                {
                    Name = "user"
                });
                var singleOrDefault = context.Users.SingleOrDefault(item=>item.UserName== "admin@test.com");
                if (singleOrDefault != null)
                    userManager.AddToRole(singleOrDefault.Id,"admin");
            }
        }
    }
}
