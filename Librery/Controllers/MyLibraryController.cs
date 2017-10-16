using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Librery.Controllers
{
    public class MyLibraryController : Controller
    {
        // GET: MyLibrary
        //public ActionResult MyLibrary()
        //{
        //    return View();
        //}

        private BookService BookService;

        public MyLibraryController()
        {
            BookService = new BookService(new Context());
        }

        protected override void Dispose(bool disposing)
        {
            BookService.Dispose();

            base.Dispose(disposing);
        }
        
        public ActionResult books_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(BookService.Read().ToDataSourceResult(request));
        }
    }
}