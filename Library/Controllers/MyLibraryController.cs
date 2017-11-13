using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Library.Services;
using Library.Data;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Library.Controllers
{
    [Authorize]
    public class MyLibraryController : Controller
    {
        private LibraryService _libraryService;

        public MyLibraryController()
        {
            _libraryService = new LibraryService();
        }

        [HttpGet]
        public ActionResult MyLibrary()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("CommonLibrary", "CommonLibrary");
            }
                return View();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult GetLibrary()
        {
            var entitys = _libraryService.GetLibrary();
            return Json(entitys, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public JsonResult DestroyLibraryItem(int id, Data.Type entityType)
        {
            if (!User.IsInRole("Admin"))
            {
                RedirectToAction("CommonLibrary", "CommonLibrary");
            }
            _libraryService.DestroyLibraryItem(id, entityType);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult UpdateLibrary(int id, Data.Type entityType)
        {
            if (!Request.IsAuthenticated || !User.IsInRole("Admin"))
            {
                RedirectToAction("CommonLibrary", "CommonLibrary");
            }
            _libraryService.UpdateLibrary(id, entityType);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [Authorize(Roles = "Admin")]
        public void GetFile(string data, string format)
        {
            var obj = new JavaScriptSerializer().DeserializeObject(data);
            //byte[] bytesInStream = FileManager.ToByteArray(obj);
            //Response.Clear();
            //Response.ContentType = "application/" + format;
            //Response.AddHeader("Content-Disposition", "attachment; filename=file." + format);
            //Response.BinaryWrite(bytesInStream);
            Response.Flush();
            Response.Close();
            Response.End();
        }


        //public List<T> GetChecked<T>()
        //{
        //    return _libraryService.GetChacked();
        //}
    }
}