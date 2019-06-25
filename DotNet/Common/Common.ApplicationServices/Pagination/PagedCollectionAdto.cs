using System.Collections.Generic;
using Common.ApplicationServices.Pagination.Interfaces;

namespace Common.ApplicationServices.Pagination
{
    public class PagedCollectionAdto<T> : IPagedCollectionAdto<T>
    {
        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
