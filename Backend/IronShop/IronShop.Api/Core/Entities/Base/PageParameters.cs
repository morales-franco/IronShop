using IronShop.Api.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Base
{
    public class PageParameters<T> 
        where T: class, new()
    {
        public int RowsPerPage { get; private set; }
        public int PageNumber { get; private set; }
        public Expression<Func<T, bool>> Filter { get; private set; }
        public Expression<Func<T, dynamic>> Sort { get; private set; }
        public List<string> Includes { get; private set; }
        public string Direction { get; private set; }
        public PageParameters(int? rowsPerPage, int? pageNumber, Expression<Func<T, bool>> filter = null, Expression<Func<T, dynamic>> sort = null, string dir = null, List<string> includes = null)
        {
            RowsPerPage = rowsPerPage??25;
            PageNumber = pageNumber??0;
            Filter = filter;
            Sort = sort;
            Includes = includes;
            Direction = string.IsNullOrEmpty(dir) ? GridDirection.Asc : (dir.ToLower() == GridDirection.Asc ? GridDirection.Asc : GridDirection.Desc);
        }



    }
}
