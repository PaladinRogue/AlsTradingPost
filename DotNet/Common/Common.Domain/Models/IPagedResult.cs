using System.Linq;

namespace Common.Domain.Models
{
    public interface IPagedResult<out T>
    {
        IQueryable<T> Items { get; }

        int TotalCount { get; }
    }
}