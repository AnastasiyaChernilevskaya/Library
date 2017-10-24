using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Services;
using Library.ViewModels;

namespace Library.Controllers
{
    public class ItemController : Controller
    {
        private ItemService _itemService;
        private BookService _bookService;

        public ItemController()
        {
            _itemService = new ItemService();
            _bookService = new BookService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(AddBookViewModel model)///??
        {
            var result = _itemService.SaveBook(model);
            if (!result)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Item");
        }

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            var result = _itemService.GetBookForEditById(id);
            if (result != null)
            {
                return View("EditBook", result);
            }
            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        public ActionResult EditBook(EditBookViewModel model)
        {
            var result = _itemService.EditBook(model);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index", "Item");
        }

        //[HttpGet]
        //public void IncludePostInHome(int bookId, bool include)
        //{
        //    _itemService.IncludeBookToHome(bookId, include);
        //}
        [HttpGet]
        public JsonResult DestroyBook(int id)
        {
            _bookService.DestroyBook(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
