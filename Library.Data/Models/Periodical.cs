using System;

namespace Library.Data
{
    [Serializable]
    public class Periodical : BaseEntity
    {
        public DateTime YearOfPublishing { get; set; }
    }
}
