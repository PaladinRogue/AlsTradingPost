using System.Collections.Generic;
using PaladinRogue.Library.Core.Api.Meta;
using PaladinRogue.Library.Core.Api.Pagination.Interfaces;
using PaladinRogue.Library.Core.Api.Resources;

namespace PaladinRogue.Library.Core.Api.Pagination
{
    public class PagedCollectionResource<T> : IPagedCollectionResource<T> where T : IResource
    {
        public IList<T> Results { get; set; }

        [ReadOnly]
        public int TotalResults { get; set; }
    }
}
