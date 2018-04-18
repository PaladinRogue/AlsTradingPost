using System.Collections.Generic;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T> : IPaginationResource
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
