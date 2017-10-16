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
        public ActionResult MyLibrary()
        {
            return View();
        }
    }
}