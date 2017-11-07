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
            CreateRolesandUsers();
        }
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //if (!roleManager.RoleExists("Admin"))
            //{
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "UserAdmin";
                user.Email = "admin1@test.com";

                string userPWD = "Test1admin";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            //}
        }
    }
}
