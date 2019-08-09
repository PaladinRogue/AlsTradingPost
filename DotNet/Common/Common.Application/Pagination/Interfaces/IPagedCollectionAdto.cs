using System.Collections.Generic;

namespace Common.Application.Pagination.Interfaces
{
    public interface IPagedCollectionAdto<T>
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
