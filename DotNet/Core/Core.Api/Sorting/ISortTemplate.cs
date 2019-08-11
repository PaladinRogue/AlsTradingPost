using System.Collections.Generic;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Common.Sorting;

namespace PaladinRogue.Libray.Core.Api.Sorting
{
    public interface ISortTemplate : ITemplate
    {
        IList<SortBy> Sort { get; set; }
    }
}
