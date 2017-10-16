using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Librery.Data.Repository
{
    class BookRepository
    {
        private static bool _updateDatabase = false;
        private Context _entities;

        public BookRepository(Context entities)
        {
            this._entities = entities;
        }

        public IList<Book> GetBooks()
        {
            IList<Book> result = new List<Book>();

            result = _entities.Books.Select(book => new Book
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                YearOfPublishing = book.YearOfPublishing,
                Publisher = book.Publisher,
            }).ToList();

            return result;
        }

        public IEnumerable<Book> Read()
        {
            return GetBooks();
        }

        public void Create(Book book)
        {
            if (!_updateDatabase)
            {
                var first = GetBooks().OrderByDescending(e => e.Id).FirstOrDefault();
                var id = (first != null) ? first.Id : 0;

                book.Id = id + 1;

                GetBooks().Insert(0, book);
            }
            else
            {
                var entity = new Book(); //ask Lesha!

                entity.Name = book.Name;
                entity.Author = book.Author;
                entity.YearOfPublishing = book.YearOfPublishing;
                entity.Publisher = book.Publisher;

                _entities.Books.Add(entity);
                _entities.SaveChanges();

                book.Id = entity.Id;
            }
        }

        public void UpdateBook(Book book)
        {
            if (!_updateDatabase)
            {
                var target = One(e => e.Id == book.Id);

                if (target != null)
                {
                    target.Name = book.Name;
                    target.YearOfPublishing = book.YearOfPublishing;
                    target.Publisher = book.Publisher;
                    target.Publisher = book.Publisher;
                }
            }
            else
            {
                var entity = new Book();

                entity.Id = book.Id;
                entity.Name = book.Name;
                entity.YearOfPublishing = book.YearOfPublishing;
                entity.Publisher = book.Publisher;

                _entities.Books.Attach(entity);
                //entities.Entry(entity).State = EntityState.Modified;
                _entities.SaveChanges();
            }
        }

        public void DestroyBook(Book book)
        {
            if (!_updateDatabase)
            {
                var target = GetBooks().FirstOrDefault(p => p.Id == book.Id);
                if (target != null)
                {
                    GetBooks().Remove(target);
                }
            }
            else
            {
                var entity = new Book();

                entity.Id = book.Id;

                _entities.Books.Attach(entity);

                _entities.Books.Remove(entity);

                _entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            _entities.Dispose();
        }


    }
}
