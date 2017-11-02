using System.Collections.Generic;
using System.Linq;


namespace Library.Data.Repositories
{
    public class BookRepository : LibraryRepository
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
            var entity = new Book();

            entity.IncludeToPage = book.IncludeToPage;
            entity.Name = book.Name;
            entity.Publisher = book.Publisher;

            entity.LibraryType = Type.Book;

            entity.Author = book.Author;
            entity.YearOfPublishing = book.YearOfPublishing;

            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var entity = GetBook(book.Id);
            entity.IncludeToPage = book.IncludeToPage;
            entity.Name = book.Name;
            entity.Publisher = book.Publisher;

            entity.Author = book.Author;
            entity.YearOfPublishing = book.YearOfPublishing;
            //entity.YearOfWriting = book.YearOfWriting;

            //_context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
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
        public List<Book> GetCheckedBooks()
        {
            var result = new List<Book>();
            foreach (Book book in GetBooks())
            {
                if (book.IncludeToPage)
                {
                     result.Add(book);
                }              
            }
            return result;
        }
    }
}

