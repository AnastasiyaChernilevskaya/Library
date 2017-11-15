using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using Library.Services;
using System.IO;


namespace Library.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public JsonResult DestroyBook(int id)
        {
            _bookService.DestroyBook(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateBook(Book book)
        {
            _bookService.UpdateBook(book);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public void GetFile(string format)
        {
            var books = new List<Book>();
            books = _bookService.GetCheckedBooks();

            byte[] bytesInStream = LibraryService.SerializeToXml(books);

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
            {
                var books = new List<Book>();
                var str = "";
                try
                {
                    using (var binaryReader = new StreamReader(Request.Files[0].InputStream))
                    {
                        str = binaryReader.ReadToEnd().ToString();
                    }
                    books = LibraryService.DeserializeFromXml<List<Book>>(str);

                    foreach (var book in books)
                    {
                        _bookService.CreateBook(book);
                    }
                    return RedirectToAction("Book");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}