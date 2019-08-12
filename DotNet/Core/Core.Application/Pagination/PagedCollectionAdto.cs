using System.Collections.Generic;
using PaladinRogue.Library.Core.Application.Pagination.Interfaces;

namespace PaladinRogue.Library.Core.Application.Pagination
{
    public class PagedCollectionAdto<T> : IPagedCollectionAdto<T>
    {
        public IList<T> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
