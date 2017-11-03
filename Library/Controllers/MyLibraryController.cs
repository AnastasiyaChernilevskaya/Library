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

namespace Library.Controllers
{
    public class MyLibraryController : Controller
    {
        private LibraryService _libraryService;

        public MyLibraryController()
        {
            _libraryService = new LibraryService();
        }

        public ActionResult MyLibrary()
        {
            return View();
        }

        public JsonResult GetLibrary()
        {
            var books = _libraryService.GetLibrary();
            return Json(books, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyLibraryItem(int id, Data.Type entityType)
        {
            _libraryService.DestroyLibraryItem(id, entityType);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateLibrary(int id, Data.Type entityType)
        {
            _libraryService.UpdateLibrary(id, entityType);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public void GetFile(string format, T entity)
        {
            var entitys = _libraryService.GetChacked<T>();

            var entitysString = _libraryService.SerializeToXml(entitys);

            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(entitysString);
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



        //public List<T> GetChecked<T>()
        //{
        //    return _libraryService.GetChacked();
        //}
    }
}