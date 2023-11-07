using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Core.Persistence.model
{
    public class PaginationResult<T>
    {

        public PaginationResult() { }

        public PaginationResult(int page, int totalPages, int pageSize, int itemCount, List<T> data)
        {
            Page = page;
            TotalPages = totalPages;
            PageSize = pageSize;
            ItemCount = itemCount;
            Data = data;
        }

        public int Page { get; set; }
        
        public int TotalPages { get; set; }
        
        public int PageSize { get; set; }

        public int ItemCount { get; set; }

        public List<T> Data { get; set; } = new List<T>();
        
    }
}