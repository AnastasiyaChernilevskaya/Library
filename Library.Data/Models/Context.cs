using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Library.Data
{
    public class Context: DbContext
    {
        public Context() : base()
        {            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
    }
}
