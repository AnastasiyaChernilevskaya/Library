using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Library.Data;


namespace Librery.Servises
{
    public class BookService: IDisposable
    {
        //private static bool UpdateDatabase = false;
        //private Context entities;

        //public BookService(Context entities)
        //{
        //    this.entities = entities;
        //}

        //public IList<BookViewModel> GetAll()
        //{
        //    IList<BookViewModel> result = new List<BookViewModel>();

        //    result = entities.Books.Select(book => new BookViewModel
        //    {
        //        Id = book.Id,
        //        Name = book.Name,
        //        Author = book.Author,
        //        YearOfPublishing = book.YearOfPublishing,
        //        Publisher = book.Publisher,
        //    }).ToList();


        //    return result;
        //}

        //public IEnumerable<BookViewModel> Read()
        //{
        //    return GetAll();
        //}

        //public void Create(BookViewModel book)
        //{
        //    if (!UpdateDatabase)
        //    {
        //        var first = GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
        //        var id = (first != null) ? first.Id : 0;

        //        book.Id = id + 1;

        //        GetAll().Insert(0, book);
        //    }
        //    else
        //    {
        //        var entity = new Book();

        //        entity.Name = book.Name;
        //        entity.Author = book.Author;
        //        entity.YearOfPublishing = book.YearOfPublishing;
        //        entity.Publisher = book.Publisher;

        //        entities.Books.Add(entity);
        //        entities.SaveChanges();

        //        book.Id = entity.Id;
        //    }
        //}

        //public void Update(BookViewModel book)
        //{
        //    if (!UpdateDatabase)
        //    {
        //        var target = One(e => e.Id == book.Id);

        //        if (target != null)
        //        {
        //            target.Name = book.Name;
        //            target.YearOfPublishing = book.YearOfPublishing;
        //            target.Publisher = book.Publisher;
        //            target.Publisher = book.Publisher;
        //        }
        //    }
        //    else
        //    {
        //        var entity = new Book();

        //        entity.Id = book.Id;
        //        entity.Name = book.Name;
        //        entity.YearOfPublishing = book.YearOfPublishing;
        //        entity.Publisher = book.Publisher;

        //        entities.Books.Attach(entity);
        //        //entities.Entry(entity).State = EntityState.Modified;
        //        entities.SaveChanges();
        //    }
        //}

        //public void Destroy(BookViewModel book)
        //{
        //    if (!UpdateDatabase)
        //    {
        //        var target = GetAll().FirstOrDefault(p => p.Id == book.Id);
        //        if (target != null)
        //        {
        //            GetAll().Remove(target);
        //        }
        //    }
        //    else
        //    {
        //        var entity = new Book();

        //        entity.Id = book.Id;

        //        entities.Books.Attach(entity);

        //        entities.Books.Remove(entity);

        //        entities.SaveChanges();
        //    }
        //}

        //public void Dispose()
        //{
        //    entities.Dispose();
        //}

    }
}