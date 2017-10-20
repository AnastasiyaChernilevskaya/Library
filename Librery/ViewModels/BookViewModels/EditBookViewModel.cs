using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.ViewModels
{
    public class EditBookViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public bool IncludeToPage { get; set; }
        public short YearOfPublishing { get; set; }
    }
}