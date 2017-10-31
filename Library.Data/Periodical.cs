using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    class Periodical : BaseEntity
    {
        public string Name { get; set; }
        public short YearOfPublishing { get; set; }
        public string Publisher { get; set; }
        public bool IncludeToPage { get; set; }
    }
}
