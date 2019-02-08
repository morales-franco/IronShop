using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Entities.Extensions
{
    public class PaginableList<T>
        where T : class, new()
    {

        public int TotalRows { get; set; }
        public List<T> Rows { get; set; }

        public PaginableList()
        {
            Rows = new List<T>();
            TotalRows = 0;
        }
    }
}
