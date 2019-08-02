using System.Collections.Generic;
using Common.Resources.Sorting;

namespace Common.ApplicationServices.Sorting
{
    public interface ISortAdto
    {
        IList<SortBy> Sort { get; set; }
    }
}
