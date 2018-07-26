using System.Collections.Generic;
using Common.Resources.Sorting;

namespace Common.Api.Sorting
{
    public interface ISortTemplate
    {
        IList<SortBy> Sort { get; set; }
    }
}
