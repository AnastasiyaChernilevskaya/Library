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
            UpdateAdmin(context);
            base.Seed(context);
        }

        private void UpdateAdmin(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roles = new List<IdentityRole>();
            roles.Add(new IdentityRole { Name = UserRole.Admin.ToString() });
            roles.Add(new IdentityRole { Name = UserRole.User.ToString() });

            foreach (var role in roles)
            {
                roleManager.Create(role);
            }

            var admin = userManager.Users.FirstOrDefault(x => x.Email == "UserAdmin@test.com");
            if (admin != null)
            {
                foreach (var role in roles)
                {
                    userManager.AddToRole(admin.Id, role.Name);
                    userManager.AddToRole(admin.Id, role.Name);
                }
            }
        }

        private void AddAdmin(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roles = roleManager.Roles.ToList();

            var admin = new ApplicationUser { Email = "UserAdmin@test.com", UserName = "UserAdmin" };

            var password = "Test1admin";
            if (userManager.Users.Any(x => x.Email == admin.Email))
            {
                return;
            }

            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                foreach (var role in roles)
                {
                    userManager.AddToRole(admin.Id, role.Name);
                    userManager.AddToRole(admin.Id, role.Name);
                }
            }
        }
    }
}
