using System.Collections.Generic;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Common.Sorting;

namespace PaladinRogue.Library.Core.Api.Sorting
{
    public interface ISortTemplate : ITemplate
    {
        IList<SortBy> Sort { get; set; }
    }
}
