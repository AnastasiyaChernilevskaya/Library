using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Library.Services;
using Library.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;

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
        //public JsonResult WriteToXML()
        //{
        //    WriteToXML();
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSerializedBook(List<Book> book)
        //{
        //    _bookService.GetSerializedBook(book);
        //    return Json(true, JsonRequestBehavior.DenyGet);
        //}

        [HttpGet]
        public FileContentResult GetXmlFile()
        {
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();
            
            var byteArray = _bookService.SerializeToXml(books);      //string presented xml
            
            //var stream = new MemoryStream();

            ////var writer = XmlWriter.Create(stream);
            //StreamWriter writer = new StreamWriter(stream);

            ////writer.WriteRaw(xml);
            //writer.Write(xml);

            ////stream.Position = 0;
            ////MemoryStream stream = new MemoryStream();
            //writer.Flush();
            //stream.Position = 0;
            var fileContentResult = File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "xml.xml");
            return fileContentResult;
        }
        
        //public void a()
        //{
        //    if (fileStream.Length == 0)
        //    {
        //        tempString =
        //            lastRecordText + recordNumber.ToString();
        //        fileStream.Write(uniEncoding.GetBytes(tempString),
        //            0, uniEncoding.GetByteCount(tempString));
        //    }
        //}

    }

}