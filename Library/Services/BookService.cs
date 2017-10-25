using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
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
        public static void WriteToXML(List<Book> books)
        {
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<Book>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationFile.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, books);
            file.Close();
        }


        public void GetSerializedBook(List<Book> books)
        {
            WriteToXML(books);
        }

        //http://www.c-sharpcorner.com/UploadFile/75a48f/how-to-use-xml-file-to-store-data-and-retrieve-data-using-as/
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
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception)
            {
                //Log exception here
            }

            return objectOut;
        }

    }
}