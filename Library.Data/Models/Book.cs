using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    [Serializable]
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public DateTime YearOfPublishing { get; set; }
        //public DateTime YearOfWriting { get; set; }
    }
}
