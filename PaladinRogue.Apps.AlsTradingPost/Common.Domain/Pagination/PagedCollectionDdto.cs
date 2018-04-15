using System;
using System.Collections.Generic;
using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Pagination
{
    public class PagedCollectionDdto<T, TOut> : IPagedCollectionDdto<T>
    {
        protected PagedCollectionDdto(IList<T> results, int totalResults, IPagination paginationDdto)
        {
            Results = results;
            TotalResults = totalResults;
            PageSize = paginationDdto.PageSize;
            PageOffset = paginationDdto.PageOffset;
        }

        public static TOut Create(IList<T> results, int totalResults, IPagination paginationDdto)
        {
            return (TOut) Activator.CreateInstance(typeof(TOut), results, totalResults, paginationDdto);
        }

        public int PageSize { get; set; }
        public int PageOffset { get; set; }
        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
