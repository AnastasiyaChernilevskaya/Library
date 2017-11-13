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

        //https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    System.Type outType = typeof(T);//

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        public string SerializeToXml(List<Book> books)
        {
            XmlSerializer ser = new XmlSerializer(books.GetType());
            string result = string.Empty;

            using (MemoryStream memStream = new MemoryStream())
            {
                ser.Serialize(memStream, books);

                memStream.Position = 0;
                result = new StreamReader(memStream).ReadToEnd();
            }
            return result;
        }

        public List<Book> GetCheckedBooks()
        {
            return _bookRepository.GetCheckedBooks();
        }
    }
}