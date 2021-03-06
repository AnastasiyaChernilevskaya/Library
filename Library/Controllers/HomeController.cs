﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Data;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        Context _context = new Context();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = _context.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

    }
}