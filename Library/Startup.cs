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

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {
            var role = new IdentityRole();
                role.Name = "Admin";

                roleManager.Create(role);

                var user = new ApplicationUser { UserName = "UserAdmin", Email = "admin1@test.com" };
                string userPWD = "Test1admin";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
