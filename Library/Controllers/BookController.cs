using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using Library.Services;
using System.IO;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        public BookController()
        {
            _bookService = new BookService();
        }

        public ActionResult Book()
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

        //public JsonResult GetBooK(int id)
        //{
        //    var book = _bookService.GetBook(id);
        //    return Json(book, JsonRequestBehavior.AllowGet);
        //}

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
            return RedirectToAction("Book");
        }

        public ActionResult EditBook(int id = 0)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return RedirectToAction("Book");
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (book != null)
            {
                _bookService.UpdateBook(book);
                return RedirectToAction("Book" );
            }
            return View(book);
        }

        public void GetFile(string format)
        {
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();

            var booksString = _bookService.SerializeToXml(books);

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(booksString);
            textWriter.Flush();

            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/" + format;
            Response.AddHeader("Content-Disposition", "attachment; filename=file." + format);
            Response.BinaryWrite(bytesInStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    //string path = Path.Combine(Server.MapPath("~/App_Data/"), Path.GetFileName(file.FileName));
                    //file.SaveAs(path);
                    List<Book> books = new List<Book>();
                    books = _bookService.DeSerializeObject<List<Book>>(file.FileName);

                    foreach (var book in books)
                    {
                        _bookService.CreateBook(book);
                    }

                    ViewBag.Message = "File uploaded successfully";
                    return RedirectToAction("MyLibrary");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

            ViewBag.Message = "You have not specified a file.";
            return View();
        }

        //https://stackoverflow.com/questions/5193842/file-upload-asp-net-mvc-3-0
        //https://www.aurigma.com/upload-suite/developers/aspnet-mvc/how-to-upload-files-in-aspnet-mvc


        public ActionResult Upload()
        {
            return View();
        }

        //________________________________________________________
        

    }
}