using System.Collections.Generic;
using Common.Api.Pagination.Interfaces;

namespace Common.Api.Pagination
{
    public class PagedCollectionResource<T> : IPagedCollectionResource<T>
    {
        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
