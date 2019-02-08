using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Extensions
{
    public class PageParameters<T> 
        where T: class, new()
    {
        public int RowsPerPage { get; private set; }
        public int PageNumber { get; private set; }
        public Expression<Func<T, bool>> Filter { get; private set; }
        public Expression<Func<T, dynamic>> Sort { get; private set; }
        public string[] Includes { get; private set; }

        public PageParameters(int rowsPerPage, int pageNumber, Expression<Func<T, bool>> filter = null, Expression<Func<T, dynamic>> sort = null, string[] includes = null)
        {
            RowsPerPage = rowsPerPage;
            PageNumber = pageNumber;
            Filter = filter;
            Sort = sort;
            Includes = includes;
        }



    }
}
