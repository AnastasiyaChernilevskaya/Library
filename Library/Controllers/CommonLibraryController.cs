using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class CommonLibraryController : Controller
    {
        private LibraryService _libraryService;

        public CommonLibraryController()
        {
            _libraryService = new LibraryService();
        }

        [HttpGet]
        public ActionResult CommonLibrary()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("MyLibrary", "MyLibrary");
            }
            return View();
        }

        public JsonResult GetCommonLibrary()
        {
            var entity = _libraryService.GetLibrary();
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
    }


}