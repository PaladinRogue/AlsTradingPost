using Common.Domain.Aggregates;

namespace Common.Application.Services.Query
{
    public interface IQueryService<T> : IGetPageQueryService<T>, IGetQueryService<T> where T : IAggregateRoot
    {
    }
}
