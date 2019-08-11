using System.Linq;

namespace PaladinRogue.Libray.Core.Domain.Models
{
    public interface IPagedResult<out T>
    {
        IQueryable<T> Items { get; }

        int TotalCount { get; }
    }
}