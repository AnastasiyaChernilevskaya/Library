using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Library;

namespace Library.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //UpdateAdmin(context);
            base.Seed(context);
        }

        //private void UpdateAdmin(ApplicationDbContext context)
        //{
        //    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    var roles = new List<IdentityRole>();
        //    roles.Add(new IdentityRole { Name = UserRole.Admin.ToString() });
        //    roles.Add(new IdentityRole { Name = UserRole.User.ToString() });

        //    foreach (var role in roles)
        //    {
        //        roleManager.Create(role);
        //    }

        //    var Admin = userManager.Users.FirstOrDefault(x => x.Email == "UserAdmin@test.com");
        //    if (Admin != null)
        //    {
        //        foreach (var role in roles)
        //        {
        //            userManager.AddToRole(Admin.Id, role.Name);
        //            userManager.AddToRole(Admin.Id, role.Name);
        //        }
        //    }
        //}            
    }
}
