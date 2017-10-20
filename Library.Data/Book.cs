﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public short YearOfPublishing { get; set; }
        public string Publisher { get; set; }
        public bool IncludeToPage { get; set; }

    }
}
