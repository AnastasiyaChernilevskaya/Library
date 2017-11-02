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

        public ActionResult EditPeriodical(int id = 0)
        {
            Periodical periodical = _periodicalService.GetPeriodical(id);
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

        public void GetFile(string format)
        {
            var periodicals = new List<Periodical>();
            periodicals = _periodicalService.GetCheckedPeriodical();

            var periodicalsString = _periodicalService.SerializeToXml(periodicals);

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(periodicalsString);
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