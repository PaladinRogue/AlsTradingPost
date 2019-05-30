using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Services.Query
{
    public interface IQueryService<T> : IGetPageQueryService<T>, IGetQueryService<T> where T : IAggregateRoot
    {
    }
}
