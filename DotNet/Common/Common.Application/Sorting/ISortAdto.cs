using System.Collections.Generic;
using Common.Resources.Sorting;

namespace Common.Application.Sorting
{
    public interface ISortAdto
    {
        IList<SortBy> Sort { get; set; }
    }
}
