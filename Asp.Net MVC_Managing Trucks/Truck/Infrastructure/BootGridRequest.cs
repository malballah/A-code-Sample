using System.Collections.Generic;

namespace Truck.Infrastructure
{
    public class BootGridRequest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchPhrase { get; set; }
        public string SortField { get; set; }
        public string SortType { get; set; }
        //extra parameters sent with the request
        public Dictionary<string, string> Params { get; set; }
    }
}