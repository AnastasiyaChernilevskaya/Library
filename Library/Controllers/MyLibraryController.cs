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
        public FileResult GetXmlFile(List<Book> books)
        {

            string xml = _bookService.SerializeToXml(books); //string presented xml
            var stream = new MemoryStream();
            //var writer = XmlWriter.Create(stream);
            //writer.WriteRaw(xml);
            //stream.Position = 0;
            //MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(xml);
            writer.Flush();
            stream.Position = 0;
            var fileStreamResult = File(stream, "application/octet-stream", "xml.xml");
            return fileStreamResult;
        }

        public JsonResult WriteToXML()
        {
            WriteToXML();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}