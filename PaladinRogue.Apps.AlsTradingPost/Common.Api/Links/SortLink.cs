using System.Collections.Generic;
using Common.Resources.Sorting;

namespace Common.Api.Links
{
    public class SortLink : Link
    {
        public IEnumerable<SortBy> Sort { get; set; }
    }
}