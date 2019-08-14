using PaladinRogue.Library.Core.Domain.Aggregates;

namespace PaladinRogue.Library.Core.Application.Services.Query
{
    public interface IQueryService<T> : IGetPageQueryService<T>, IGetQueryService<T> where T : IAggregateRoot
    {
    }
}
