using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using Library.Services;
using System.IO;
using Library.ViewModels.Book;

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
                    file.InputStream.Position = 0;
                    var books = new List<Book>();
                    //books = FileManager.FromFile<List<Book>>(file);
                    var model = new UploadedBooksViewModel
                    {
                        Books = Json(books)
                    };
                    return RedirectToAction("UploadedBooks", new { model = model });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }

            ViewBag.Message = "You have not specified a file.";
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult UploadedBooks(UploadedBooksViewModel model)
        {
            return View(model);
        }
        //________________________________________________________
        

    }
}