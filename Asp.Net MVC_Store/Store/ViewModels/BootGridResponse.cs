using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;

namespace Store.ViewModels
{
    public class BootGridResponse<T> where T : class
    {
        private readonly BootGridRequest _request;
        private readonly IEnumerable<T> _rows;
        public BootGridResponse(BootGridRequest request, IEnumerable<T> rows)
        {
            this._request = request;
            this._rows = rows;
        }
        public int CurrentPage => this._request.CurrentPage; // current page
        public int PageSize => this._request.PageSize; // rows per page 
        public int Total => _rows.Count(); // total rows for whole query
        public IEnumerable<T> Rows => OrderItems()
                .Skip((CurrentPage - 1) * PageSize) //skip the previous page
                .Take(PageSize);//take number of items // items

        private IEnumerable<T> OrderItems()
        {
            if (string.IsNullOrEmpty(_request.SortField))
                return _rows.AsEnumerable();
            return _request.SortType == "asc" ? _rows.OrderBy(_request.SortField) : _rows.OrderBy(_request.SortField + " descending");
        }
    }
}