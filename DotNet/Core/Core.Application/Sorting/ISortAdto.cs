using System.Collections.Generic;
using PaladinRogue.Libray.Core.Common.Sorting;

namespace PaladinRogue.Libray.Core.Application.Sorting
{
    public interface ISortAdto
    {
        IList<SortBy> Sort { get; set; }
    }
}
