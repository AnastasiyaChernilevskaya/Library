using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Services;
using Library.Data.Repository;
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
        protected override void Dispose(bool disposing)
        {
            _bookService.Dispose();

            base.Dispose(disposing);
        }
        
        //public ActionResult books_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(_bookService.Read().ToDataSourceResult(request));
        //}
    }
}