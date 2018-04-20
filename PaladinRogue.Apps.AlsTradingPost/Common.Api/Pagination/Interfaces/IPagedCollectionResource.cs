using System.Collections.Generic;
using Common.Api.Resources;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T> where T : ISummaryResource
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
