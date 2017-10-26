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
            XmlSerializer writer = new XmlSerializer(typeof(List<Book>));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationFile.xml";
            FileStream file = File.Create(path);

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
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

    }
}

//// Create a writer that outputs to the console.
//XmlTextWriter writer = new XmlTextWriter(Console.Out);
//writer.Formatting = Formatting.Indented;

//            // Write the root element.
//            writer.WriteStartElement("Items");

//            // Write a string using WriteRaw. Note that the special
//            // characters are not escaped.
//            writer.WriteStartElement("Item");
//            writer.WriteString("Write unescaped text:  ");
//            writer.WriteRaw("this & that");
//            writer.WriteEndElement();

//            // Write the same string using WriteString. Note that the 
//            // special characters are escaped.
//            writer.WriteStartElement("Item");
//            writer.WriteString("Write the same string using WriteString:  ");
//            writer.WriteString("this & that");
//            writer.WriteEndElement();

//            // Write the close tag for the root element.
//            writer.WriteEndElement();

//            // Write the XML to file and close the writer.
//            writer.Close();