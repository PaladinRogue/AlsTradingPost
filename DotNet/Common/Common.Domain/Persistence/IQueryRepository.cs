using Common.Domain.Models.Interfaces;

namespace Common.Domain.Persistence
{
    public interface IQueryRepository<T> : IGetById<T>, IGetPage<T>, IGet<T>, IGetSingle<T>, ICheckExists, ICheckConcurrency where T : IVersionedEntity
    {
    }
}
