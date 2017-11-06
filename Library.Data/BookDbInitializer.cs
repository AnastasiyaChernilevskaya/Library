using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Data
{
    public class BookDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", Publisher = "veselka", IncludeToPage = true, YearOfPublishing = DateTime.Now,  LibraryType = Type.Book });
            context.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Publisher = "veselka", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            context.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Publisher = "veselka", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            context.Books.Add(new Book { Name = "Война и мир1", Author = "Л. Толстой1", Publisher = "veselkas", IncludeToPage = true, YearOfPublishing = DateTime.Now,  LibraryType = Type.Book });
            context.Books.Add(new Book { Name = "Отцы и дети1", Author = "И. Тургенев1", Publisher = "veselkas", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            context.Books.Add(new Book { Name = "Чайка1", Author = "А. Чехов1", Publisher = "veselkas", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });

            context.Newspapers.Add(new Newspaper { Name = "1sdfg", Publisher = "aerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });
            context.Newspapers.Add(new Newspaper { Name = "2sdfg", Publisher = "aerg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });
            context.Newspapers.Add(new Newspaper { Name = "3sdfg", Publisher = "aerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });

            context.Periodicals.Add(new Periodical { Name = "1rdghaersdfg", Publisher = "straerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            context.Periodicals.Add(new Periodical { Name = "2sstrjdfg", Publisher = "aerstrjg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            context.Periodicals.Add(new Periodical { Name = "3sstrjdfg", Publisher = "aersrtjg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            context.Periodicals.Add(new Periodical { Name = "4sdstrjfg", Publisher = "aesrtjrg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            context.Periodicals.Add(new Periodical { Name = "5sstrjdfg", Publisher = "aaherahg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "admin@test.com", UserName = "admin" };
            string password = "Admin1!";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}