using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Application.Pagination.Interfaces
{
    public interface IPagedCollectionAdto<T>
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
