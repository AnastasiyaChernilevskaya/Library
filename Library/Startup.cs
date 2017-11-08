using System;
using Library.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web;
using System.IO;
using Microsoft.Owin.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(Library.Startup))]
namespace Library
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ////if (!roleManager.RoleExists("Admin"))
            ////{
            ////var role = new IdentityRole();
            ////    role.Name = "Admin";

            ////    roleManager.Create(role);

            //    var user = new ApplicationUser { UserName = "UserAdmin", Email = "Admin1@test.com" };
            //    string userPWD = "Test1Admin";

            //    var chkUser = UserManager.Create(user, userPWD);

            //    if (chkUser.Succeeded)
            //    {
            //        var result1 = UserManager.AddToRole(user.Id, "Admin");
            //    }
            ////}
            if (!roleManager.RoleExists("Admin"))
            {
                var role1 = new IdentityRole { Name = "Admin" };
                roleManager.Create(role1);

                var Admin = new ApplicationUser { Email = "1Admin@test.com", UserName = "1Admin@test.com" };
                string password = "Test1user!";
                var result = userManager.Create(Admin, password);


                if (result.Succeeded)
                {
                    userManager.AddToRole(Admin.Id, role1.Name);

                }
            }

            if (!roleManager.RoleExists("user"))
            {
                var role2 = new IdentityRole { Name = "user" };
                roleManager.Create(role2);

                var user = new ApplicationUser { Email = "1user@test.com", UserName = "1user@test.com" };
                string password = "Test1user!";
                var result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, role2.Name);
                }

            }           
        }
    }
}
