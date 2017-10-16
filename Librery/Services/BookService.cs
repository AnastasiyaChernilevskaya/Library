using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Library.Data;
using Library.Data.Repository;

namespace Library.Services
{
    public class BookService //IDisposable?
    {
        private BookRepository _bookRepository;
        public BookService()
        {
            _bookRepository = new BookRepository();        
        }

        public IList<Book> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        public IEnumerable<Book> Read()
        {
            return _bookRepository.Read();
        }

        public void Create(Book book)
        {
            _bookRepository.Create(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
        }

        public void DestroyBook(Book book)
        {
            _bookRepository.DestroyBook(book);
        }        

        public void Dispose()
        {
            _bookRepository.Dispose();
        }
    }
}