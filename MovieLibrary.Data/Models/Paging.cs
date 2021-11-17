using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLibrary.Data.Models
{
    public class Paging
    {
        public int maxPageSize = 55;
        public int PageNumber { get; set; } = 1;
        public int pageSize = 5;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
