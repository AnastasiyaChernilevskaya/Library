using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IncludeToPage { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }

        public Type LibraryType { get; set; }
    }
}
