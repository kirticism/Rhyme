using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rhyme.Domain.Entities.Pagination
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public T Data { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, Uri previousPage, Uri nextPage)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.PreviousPage = previousPage;
            this.NextPage = nextPage;
            this.TotalRecords = TotalRecords;
        }
        public PagedResponse(T data, int pageNumber, int pageSize, Uri previousPage, Uri nextPage, int totalRecord)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.PreviousPage = previousPage;
            this.NextPage = nextPage;
            this.TotalRecords = totalRecord;
        }
    }
}