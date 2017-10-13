using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Library.Models
{
    public class BookViewModel
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [DisplayName("book name")]
        [DataType("String")]
        public string Name
        {
            get;
            set;
        }

        [Required]
        [DisplayName("book name")]
        [DataType("String")]
        public string Author
        {
            get;
            set;
        }


        [DisplayName("YearOfPublishing")]
        [DataType(DataType.Date)]
        public DateTime YearOfPublishing
        {
            get;
            set;
        }

        [DisplayName("Publisher")]
        [DataType("String")]
        public String Publisher
        {
            get;
            set;
        }

    }
}