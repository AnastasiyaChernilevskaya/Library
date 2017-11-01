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
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой",  Publisher = "veselka", IncludeToPage = true });
            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев",  Publisher = "veselka", IncludeToPage = false });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов",  Publisher = "veselka", IncludeToPage = false });
            db.Books.Add(new Book { Name = "Война и мир1", Author = "Л. Толстой1",  Publisher = "veselkas", IncludeToPage = true });
            db.Books.Add(new Book { Name = "Отцы и дети1", Author = "И. Тургенев1",  Publisher = "veselkas", IncludeToPage = false });
            db.Books.Add(new Book { Name = "Чайка1", Author = "А. Чехов1", Publisher = "veselkas", IncludeToPage = true });

            base.Seed(db);
        }
    }
}