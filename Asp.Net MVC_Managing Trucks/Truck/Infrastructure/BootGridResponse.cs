using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Truck.Infrastructure
{
    //a response to a bootgrid request
    public class BootGridResponse<T> where T : class
    {
        //bootgrid received request
        private readonly BootGridRequest _request;
        //data rows
        private IEnumerable<T> _allRows;
        private IEnumerable<T> _rows;
        public BootGridResponse(BootGridRequest request, IEnumerable<T> rows)
        {
            _request = request;
            _allRows = rows;
            Rows = rows;
        }
        //current page number received from request
        public int CurrentPage => _request.CurrentPage; // current page
        //page size received from request
        public int PageSize => _request.PageSize;// rows per page 
        //total number of rows
        public int Total => _allRows.Count(); // total rows for whole query
        //take only the current page from all rows
        public IEnumerable<T> Rows
        {
            get { return _rows; }
            set
            {
                //set all rows which not fetched from database
                _allRows = value;
                //fetch required rows only from database
                _rows= PageSize > 0 ? OrderItems()
                .Skip((CurrentPage - 1) * PageSize) //skip the previous page
                .Take(PageSize).ToList()//take number of items // items
                : OrderItems();
            }
        }
       
        //order items
        private IEnumerable<T> OrderItems()
        {
            if (string.IsNullOrEmpty(_request.SortField))
                return _allRows.AsEnumerable();
            return _request.SortType == "asc" ? _allRows.OrderBy(_request.SortField) : _allRows.OrderBy(_request.SortField + " descending");
        }
        //to sort only the selected rows
        public void SortFinalList(string sortField,string sortType="asc")
        {
            _rows= sortType == "asc" ? _rows.OrderBy(sortField) : _rows.OrderBy(sortField + " descending");
        }
    }
}