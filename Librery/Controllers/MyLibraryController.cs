using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Services;
using Library.Data;

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

        public JsonResult DestroyBook (int id)
        {
            _bookService.DestroyBook(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateBook(Book book)
        {
            _bookService.UpdateBook(book);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateBook(Book book)
        {
            _bookService.CreateBook(book);
            return Json(true, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetBooK(int id)
        {
            var book = _bookService.GetBook(id);
            return Json(book, JsonRequestBehavior.AllowGet);
        }
    }
}