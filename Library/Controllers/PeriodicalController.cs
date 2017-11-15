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
    public class PeriodicalController : Controller
    {
        private PeriodicalService _periodicalService;

        public PeriodicalController()
        {
            _periodicalService = new PeriodicalService();
        }
        public ActionResult Periodical()
        {
            return View();
        }

        public JsonResult GetPeriodicals()
        {
            var periodicals = _periodicalService.GetPeriodicals();
            return Json(periodicals, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult DestroyPeriodical(int id)
        {
            _periodicalService.DestroyPeriodical(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePeriodical(Periodical periodical)
        {
            _periodicalService.UpdatePeriodical(periodical);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddPeriodical()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPeriodical(Periodical periodical)
        {
            _periodicalService.CreatePeriodical(periodical);
            return RedirectToAction("Periodical");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditPeriodical(int id = 0)
        {
            var periodical = _periodicalService.GetPeriodical(id);
            if (periodical == null)
            {
                return RedirectToAction("Periodical");
            }
            return View(periodical);
        }

        [HttpPost]
        public ActionResult EditPeriodical(Periodical periodical)
        {
            if (periodical != null)
            {
                _periodicalService.UpdatePeriodical(periodical);
                return RedirectToAction("Periodical");
            }
            return View(periodical);
        }

        [Authorize(Roles = "Admin")]
        public void GetFile(string format)
        {
            var periodicals = new List<Periodical>();
            periodicals = _periodicalService.GetCheckedPeriodical();           

            byte[] bytesInStream = LibraryService.SerializeToXml(periodicals);

            Response.Clear();
            Response.ContentType = "application/" + format;
            Response.AddHeader("Content-Disposition", "attachment; filename=file." + format);
            Response.BinaryWrite(bytesInStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }

        [HttpPost]
        public ActionResult UploadPeriodical(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var periodicals = new List<Periodical>();
                var str = "";
                try
                {
                    using (var binaryReader = new StreamReader(Request.Files[0].InputStream))
                    {
                        str = binaryReader.ReadToEnd().ToString();
                    }
                    periodicals = LibraryService.DeserializeFromXml<List<Periodical>>(str);

                    foreach (var periodical in periodicals)
                    {
                        _periodicalService.CreatePeriodical(periodical);
                    }
                    return RedirectToAction("Periodical");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            return View();
        }

        public ActionResult UploadPeriodical()
        {
            return View();
        }
    }
}