using System.Collections.Generic;
using System.Linq;
using Common.Resources.Sorting;

namespace Common.Api.Formats.JsonV1.Extensions
{
    public static class SortByExtensions
    {
        public static string ToCommaSeperatedString(this IEnumerable<SortBy> sortBy)
        {
            IEnumerable<string> collection = sortBy.Select(s => $"{(s.IsAscending ? string.Empty : "-")}{s.PropertyName.ToLowerInvariant()}");

            return string.Join(",", collection);
        }
    }
}
