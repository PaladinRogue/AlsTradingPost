using Common.Domain.Models.Interfaces;

namespace Common.Domain.Persistence
{
    public interface IQueryRepository<T> : IGetByIdQuery<T>, IGetPageQuery<T>, IGetQuery<T>, IGetSingleQuery<T>, ICheckExistsQuery<T>, ICheckConcurrencyQuery<T> where T : IVersionedEntity
    {
    }
}
