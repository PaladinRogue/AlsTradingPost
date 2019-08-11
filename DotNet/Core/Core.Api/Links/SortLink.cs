using System.Collections.Generic;
using PaladinRogue.Libray.Core.Common.Sorting;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public class SortLink : Link
    {
        public IEnumerable<SortBy> Sort { get; set; }
    }
}