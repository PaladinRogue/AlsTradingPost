using System.Collections.Generic;
using Common.Api.Resources;
using Common.Resources.Sorting;

namespace Common.Api.Sorting
{
    public interface ISortTemplate : ITemplate
    {
        IList<SortBy> Sort { get; set; }
    }
}
