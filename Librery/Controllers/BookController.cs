using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Services;
using Library.ViewModels;

namespace Librery.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated )
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(AddBookViewModel model)
        {
            if (!Request.IsAuthenticated )
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _bookService.SaveBook(model);
            if (!result)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public ActionResult EditPost(string id)
        {
            if (!Request.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _bookService.GetPostForEditById(id);
            if (result != null)
            {
                return View("EditPost", result);
            }
            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public ActionResult EditPost(EditBookViewModel model)
        {
            if (!Request.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _bookService.EditBook(model);
            if (!result)
            {
                return View();
            }
            return RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public ActionResult GetPostsForAdmin()
        {
            if (!Request.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _bookService.GetPostsForAdmin();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void IncludePostInHome(int bookId, bool include)
        {
            _bookService.IncludeBookToHome(bookId, include);
        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            if (!Request.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _bookService.DeleteBook(id);
            return RedirectToAction("Index", "Post");
        }
    }
}
