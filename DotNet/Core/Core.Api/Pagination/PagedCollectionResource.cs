using System.Collections.Generic;
using PaladinRogue.Libray.Core.Api.Meta;
using PaladinRogue.Libray.Core.Api.Pagination.Interfaces;
using PaladinRogue.Libray.Core.Api.Resources;

namespace PaladinRogue.Libray.Core.Api.Pagination
{
    public class PagedCollectionResource<T> : IPagedCollectionResource<T> where T : IResource
    {
        public IList<T> Results { get; set; }

        [ReadOnly]
        public int TotalResults { get; set; }
    }
}
