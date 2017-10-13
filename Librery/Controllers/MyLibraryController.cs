using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Library.Controllers
{
    public class MyLibraryController : Controller
    {
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

        [Demo]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult books_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(BookService.Read().ToDataSourceResult(request));
        }

    }
}