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
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой", YearOfPublishing  = 2017, Publisher = "veselka"});
            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев", YearOfPublishing = 2017, Publisher = "veselka" });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", YearOfPublishing = 2017, Publisher = "veselka" });

            base.Seed(db);
        }
    }
}