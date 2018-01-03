using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels
{
    public class BootGridResponse<T> where T : class
    {
        public int Current { get; set; } // current page
        public int RowCount { get; set; } // rows per page
        public IEnumerable<T> Rows { get; set; } // items
        public int Total { get; set; } // total rows for whole query
    }
}