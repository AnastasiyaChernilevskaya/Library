using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class BookRepository : BaseRepository
    {
        private Context _context;

        public BookRepository()
        {
            _context = new Context();
        }

        public IList<Book> GetBooks()
        {
            var result = new List<Book>();

            result = _context.Books.ToList();

            return result;
        }

        public void CreateBook(Book book)
        {
            var entity = new Book(); //ask Lesha!

            entity.Name = book.Name;
            entity.Author = book.Author;
            entity.YearOfPublishing = book.YearOfPublishing;
            entity.Publisher = book.Publisher;
            entity.IncludeToPage = book.IncludeToPage;

            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var entity = new Book();

            entity.Id = book.Id;
            entity.Name = book.Name;
            entity.YearOfPublishing = book.YearOfPublishing;
            entity.Publisher = book.Publisher;
            entity.Author = book.Author;
            entity.IncludeToPage = book.IncludeToPage;

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DestroyBook(int id)
        {
            _context.Books.Remove(GetBook(id));
            _context.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return GetBooks().FirstOrDefault(p => p.Id == id);
        }
    }
}

