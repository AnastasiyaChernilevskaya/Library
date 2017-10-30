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
        //public JsonResult WriteToXML()
        //{
        //    _bookService.WriteToXML();
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSerializedBook(List<Book> book)
        //{
        //    _bookService.GetSerializedBook(book);
        //    return Json(true, JsonRequestBehavior.DenyGet);
        //}

        [HttpGet]
        public void GetXmlFile()
        {
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();
            //var booksString = _bookService.SerializeToXml(books);

            //MemoryStream memoryStream = new MemoryStream();
            //TextWriter textWriter = new StreamWriter(memoryStream);
            //textWriter.WriteLine(booksString);
            //textWriter.Flush(); // added this line

            //byte[] bytesInStream = memoryStream.ToArray(); // simpler way of converting to array
            //memoryStream.Close();

            //Response.Clear();
            //Response.ContentType = "application/xml";
            //Response.AddHeader("content-disposition", "attachment;    filename=file.xls");
            //Response.BinaryWrite(bytesInStream);
            //Response.Flush();
            //Response.Close();

            //Response.End();
            byte[] byteArray = Encoding.UTF8.GetBytes(_bookService.SerializeToXml(books));
            Stream file = new MemoryStream(byteArray);
            var filename = "file.xls";
            if (file != null && file.CanRead)
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                Response.ContentType = "application/octet-stream";
                Response.ClearContent();
                file.CopyTo(Response.OutputStream);
            }

        }

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

        //var books = new List<Book>();
        //books = _bookService.GetCheckedBooks();
        //    _bookService.SerializeToXml(books);
        //    byte[] byteArray = Encoding.UTF8.GetBytes(_bookService.SerializeToXml(books));
        //var mimeType = "text/xml";
        //var fileResult = new FileContentResult(byteArray, mimeType);
        //fileResult.FileDownloadName = "myfile";
        //    return fileResult;


        //protected void DisplayDownloadDialog()
        //{
        //    Response.Clear();
        //    Response.AddHeader(
        //        "content-disposition", string.Format("attachment; filename={0}", "filename.xml"));

        //    Response.ContentType = "application/octet-stream";

        //    Response.WriteFile("filename.xml");
        //    Response.End();
        //}
    }

}