using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyme.Domain.Entities.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string applicationPath { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.applicationPath = "";
        }
        public PaginationFilter(int pageNumber, int pageSize, string applicationPath)
        {
            this.PageNumber = pageNumber >= 0 ? pageNumber : 1;
            this.PageSize = pageSize >= 0 ? pageSize : 10;
            this.applicationPath = applicationPath;
        }
    }
}