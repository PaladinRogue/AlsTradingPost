using Common.Domain.Models.Interfaces;

namespace Common.Domain.Services.Query
{
    public interface IQueryService<T> : IGetByIdQueryService<T>, IGetSingleQueryService<T>, IGetPageQueryService<T>, IGetQueryService<T>, ICheckExistsQueryService, ICheckConcurrencyQueryService where T : IEntity
    {
    }
}
