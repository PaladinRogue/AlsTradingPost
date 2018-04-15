using System.Collections.Generic;

namespace Common.Application.Pagination.Interfaces
{
    public interface IPagedCollectionAdto<T> : IPagintationAdto
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
