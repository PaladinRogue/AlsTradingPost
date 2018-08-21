using System.Collections.Generic;
using System.Linq;
using Common.Resources.Sorting;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Api.Formats.JsonV1.Extensions
{
    public static class SortByExtensions
    {
        public static string ToCommaSeperatedString(this IEnumerable<SortBy> sortBy)
        {
            return sortBy.Select(s => $"{(s.IsAscending ? string.Empty : "-")}{s.PropertyName.ToLowerInvariant()}").Join(",");
        }
    }
}
