using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels
{
    public class BootGridRequest
    {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public string SearchPhrase { get; set; }
        public string SortField { get; set; }
        public string SortType { get; set; }
       
    }
}