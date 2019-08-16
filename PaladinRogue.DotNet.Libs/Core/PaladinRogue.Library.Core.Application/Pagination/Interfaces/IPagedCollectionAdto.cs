using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Application.Pagination.Interfaces
{
    public interface IPagedCollectionAdto<T>
    {
        IList<T> Results { get; set; }
        int TotalResults { get; set; }
    }
}
