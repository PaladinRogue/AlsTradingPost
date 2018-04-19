using System.Collections.Generic;

namespace Common.Domain.Pagination.Interfaces
{
    public interface IPagedCollectionDdto<T>
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
