using System.Collections.Generic;
using PaladinRogue.Library.Core.Common.Sorting;

namespace PaladinRogue.Library.Core.Application.Sorting
{
    public interface ISortAdto
    {
        IList<SortBy> Sort { get; set; }
    }
}
