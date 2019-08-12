using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.Pagination.Interfaces
{
    public interface IPagedCollectionDdto<T>
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
