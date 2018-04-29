using System.Collections.Generic;
using Common.Api.Meta;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Pagination
{
    public class PagedCollectionResource<T> : IPagedCollectionResource<T> where T : ISummaryResource
    {
        public IList<T> Results { get; set; }
        [ReadOnly]
        public int TotalResults { get; set; }
    }
}
