using System.Linq;

namespace Common.Domain.Models
{
    public class PagedResult<T> : IPagedResult<T>
    {
        private PagedResult(IQueryable<T> items,
            int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IQueryable<T> Items { get; }

        public int TotalCount { get; }

        public static IPagedResult<T> Create(IQueryable<T> items,
            int totalCount)
        {
            return new PagedResult<T>(items, totalCount);
        }
    }
}