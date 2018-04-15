using System.Collections.Generic;

namespace Common.Domain.Pagination.Interfaces
{
    public interface IPagedCollectionDdto<T> : IPagination
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
