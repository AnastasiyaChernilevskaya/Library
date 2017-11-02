using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Library.Data
{
    public class BookDbInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", Publisher = "veselka", IncludeToPage = true, YearOfPublishing = DateTime.Now,  LibraryType = Type.Book });
            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", Publisher = "veselka", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Publisher = "veselka", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            db.Books.Add(new Book { Name = "Война и мир1", Author = "Л. Толстой1", Publisher = "veselkas", IncludeToPage = true, YearOfPublishing = DateTime.Now,  LibraryType = Type.Book });
            db.Books.Add(new Book { Name = "Отцы и дети1", Author = "И. Тургенев1", Publisher = "veselkas", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });
            db.Books.Add(new Book { Name = "Чайка1", Author = "А. Чехов1", Publisher = "veselkas", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Book });

            db.Newspapers.Add(new Newspaper { Name = "1sdfg", Publisher = "aerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });
            db.Newspapers.Add(new Newspaper { Name = "2sdfg", Publisher = "aerg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });
            db.Newspapers.Add(new Newspaper { Name = "3sdfg", Publisher = "aerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Newspaper });

            db.Periodicals.Add(new Periodical { Name = "1rdghaersdfg", Publisher = "straerg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            db.Periodicals.Add(new Periodical { Name = "2sstrjdfg", Publisher = "aerstrjg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            db.Periodicals.Add(new Periodical { Name = "3sstrjdfg", Publisher = "aersrtjg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            db.Periodicals.Add(new Periodical { Name = "4sdstrjfg", Publisher = "aesrtjrg", IncludeToPage = false, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });
            db.Periodicals.Add(new Periodical { Name = "5sstrjdfg", Publisher = "aaherahg", IncludeToPage = true, YearOfPublishing = DateTime.Now, LibraryType = Type.Periodical });

            base.Seed(db);
        }
    }
}