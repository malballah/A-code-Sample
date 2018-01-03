using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.ViewModels
{
    public class BootGridRequest
    {
    
            public int current { get; set; }
            public int rowCount { get; set; }
            public string searchPhrase { get; set; }
       
    }
}