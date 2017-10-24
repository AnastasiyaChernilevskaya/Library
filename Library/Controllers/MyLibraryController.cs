using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Services;
using Library.Data;
using System.Data.Entity;

namespace Library.Controllers
{
    public class MyLibraryController : Controller
    {
        private BookService _bookService;
        private Context _context;

        public MyLibraryController()
        {
            _bookService = new BookService();
            _context = new Context();
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


        //[HttpPost]
        //public ActionResult EditBook(EditBookViewModel model)
        //{
        //    var result = _itemService.EditBook(model);
        //    if (!result)
        //    {
        //        return View();
        //    }
        //    return RedirectToAction("Index", "Item");
        //}


        public JsonResult UpdateBook(Book book)
        {
            _bookService.UpdateBook(book);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        //public ActionResult EditBook(Book book)
        //{
        //    MyLibraryController book = ;
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}

        [HttpPost]
        public ActionResult AddBook(MyLibraryController book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(MyLibraryController book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                //db.Entry(book).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}