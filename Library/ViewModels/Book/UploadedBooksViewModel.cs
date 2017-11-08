using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.ViewModels.Book
{
    public class UploadedBooksViewModel
    {
        public JsonResult Books { get; set; }
    }
}