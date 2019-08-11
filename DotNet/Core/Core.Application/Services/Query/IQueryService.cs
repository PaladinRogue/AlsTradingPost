using PaladinRogue.Libray.Core.Domain.Aggregates;

namespace PaladinRogue.Libray.Core.Application.Services.Query
{
    public interface IQueryService<T> : IGetPageQueryService<T>, IGetQueryService<T> where T : IAggregateRoot
    {
    }
}
