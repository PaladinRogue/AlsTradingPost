using System.Collections.Generic;
using PaladinRogue.Library.Core.Common.Sorting;

namespace PaladinRogue.Library.Core.Api.Links
{
    public class SortLink : Link
    {
        public IEnumerable<SortBy> Sort { get; set; }
    }
}