using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;

namespace Library.ViewModels
{
    public class UploadedBooksViewModel
    {
        public List<Book> Books { get; set; }
    }
}