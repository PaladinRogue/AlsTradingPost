using Common.Domain.Models.Interfaces;

namespace Common.Domain.Persistence
{
    public interface IRepository<T> : IGetById<T>, IGetPage<T>, IGet<T>, IGetSingle<T>, IUpdate<T>, IAdd<T>, IDelete, ICheckExists, ICheckConcurrency where T : IVersionedEntity
    {
    }
}
