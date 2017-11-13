using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;
using Library.Services;
using System.IO;

namespace Library.Controllers
{
    [Authorize]
    public class NewspaperController : Controller
    {
        private NewspaperService _newspaperService;

        public NewspaperController()
        {
            _newspaperService = new NewspaperService();
        }
        public ActionResult Newspaper()
        {
            return View();
        }

        public JsonResult GetNewspapers()
        {
            var newspapers = _newspaperService.GetNewspapers();
            return Json(newspapers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyNewspaper(int id)
        {
            _newspaperService.DestroyNewspaper(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateNewspaper(Newspaper newspaper)
        {
            _newspaperService.UpdateNewspaper(newspaper);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult AddNewspaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewspaper(Newspaper newspaper)
        {
            _newspaperService.CreateNewspaper(newspaper);
            return RedirectToAction("Newspaper");
        }

        public ActionResult EditNewspaper(int id = 0)
        {
            Newspaper newspaper = _newspaperService.GetNewspaper(id);
            if (newspaper == null)
            {
                return RedirectToAction("Newspaper");
            }
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult EditNewspaper(Newspaper newspaper)
        {
            if (newspaper != null)
            {
                _newspaperService.UpdateNewspaper(newspaper);
                return RedirectToAction("Newspaper");
            }
            return View(newspaper);
        }

        public void GetFile(string format)
        {
            var newspapers = new List<Newspaper>();
            newspapers = _newspaperService.GetCheckedNewspapers();

            var newspapersString = _newspaperService.SerializeToXml(newspapers);

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(newspapersString);
            textWriter.Flush();

            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/" + format;
            Response.AddHeader("Content-Disposition", "attachment; filename=file." + format);
            Response.BinaryWrite(bytesInStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }
    }
}