using System;
using System.Collections.Generic;
using Library.Data;
using Library.Data.Repositories;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Library.Services
{
    public class BookService
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
        public void CreateBook(Book book)
        {
            _bookRepository.CreateBook(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
        }

        public void DestroyBook(int id)
        {
            _bookRepository.DestroyBook(id);
        }
        public Book GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }

        public List<Book> GetCheckedBooks()
        {
            return _bookRepository.GetCheckedBooks();
        }
    }
}