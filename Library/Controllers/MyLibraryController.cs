using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Library.Services;
using Library.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;

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

        public void GetXmlFile(string format)
        {
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();

            var booksString = _bookService.SerializeToXml(books); // 

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(booksString);
            textWriter.Flush();

            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/"+ format;
            Response.AddHeader("Content-Disposition", "attachment; filename=file." + format);
            Response.BinaryWrite(bytesInStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }

        //
        public JsonResult WriteToXML(string fileName)
        {
            string fileN = fileName.Replace("\"", "");
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();
            byte[] byteArray = Encoding.UTF8.GetBytes(_bookService.SerializeToXml(books));
            Stream file = new MemoryStream(byteArray);
            if (file != null && file.CanRead)
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileN + "\"");
                Response.ContentType = "application/octet-stream";
                Response.ClearContent();
                file.CopyTo(Response.OutputStream);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }

}