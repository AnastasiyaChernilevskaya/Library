using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Library.Services;
using Library.Data;
using System.Xml;
using System.IO;

namespace Library.Controllers
{
    public class MyLibraryController : Controller
    {
        private BookService _bookService;

        public MyLibraryController()
        {
            _bookService = new BookService();
        }


        public ActionResult MyLibrary()
        {
            return View();
        }

        public JsonResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Json(books, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DestroyBook(int id)
        {
            _bookService.DestroyBook(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult CreateBook(Book book)
        //{
        //    _bookService.CreateBook(book);
        //    return Json(true, JsonRequestBehavior.DenyGet);
        //}

        public JsonResult GetBooK(int id)
        {
            var book = _bookService.GetBook(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateBook(Book book)
        {
            _bookService.UpdateBook(book);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            _bookService.CreateBook(book);
            return RedirectToAction("MyLibrary");
        }
        public ActionResult EditBook(int id = 0)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return RedirectToAction("MyLibrary");
            }
            return View(book);
        }


        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (book != null)
            {
                _bookService.UpdateBook(book);
                return RedirectToAction("MyLibrary");
            }
            return View(book); //book
        }

        public JsonResult GetSerializedBook(List<Book> book)
        {
            _bookService.GetSerializedBook(book);
            return Json(true, JsonRequestBehavior.DenyGet);
        }
        public FileResult GetXmlFile()
        {
            string xml = ""; //string presented xml
            var stream = new MemoryStream();
            var writer = XmlWriter.Create(stream);
            writer.WriteRaw(xml);
            stream.Position = 0;
            var fileStreamResult = File(stream, "application/octet-stream", "xml1.xml");
            return fileStreamResult;
        }

        public void ExportXML()
        {
            XmlDocument doc = new XmlDocument();

            // XML declaration
            XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            doc.AppendChild(declaration);

            // Root element: Catalog
            XmlElement root = doc.CreateElement("Catalog");
            doc.AppendChild(root);

            // Sub-element: srsapiversion of root
            XmlElement book = doc.CreateElement("book");
            root.AppendChild(book);


            // Sub-element: srsapiversion of root
            XmlElement bookname = doc.CreateElement("bookname");
            book.InnerText = "2 States";
            root.AppendChild(bookname);

            // Sub-element: id of root
            XmlElement id = doc.CreateElement("id");
            id.InnerText = "70-515";
            root.AppendChild(id);

            // Sub-element: author of root
            XmlElement author = doc.CreateElement("author");
            author.InnerText = "Chetan Bhagat";
            //Attribute age of author
            XmlAttribute age = doc.CreateAttribute("age");
            age.Value = "43";
            author.Attributes.Append(age);
            root.AppendChild(author);

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

            doc.WriteTo(writer);
            writer.Flush();
            Response.Clear();
            byte[] byteArray = stream.ToArray();
            Response.AppendHeader("Content-Disposition", "filename=Books.xml");
            Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(byteArray);
            writer.Close();
        }

        public JsonResult WriteToXML()
        {
            WriteToXML();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}