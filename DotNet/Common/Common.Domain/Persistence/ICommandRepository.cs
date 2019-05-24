using Common.Domain.Models.Interfaces;

namespace Common.Domain.Persistence
{
    public interface ICommandRepository<in T> : IUpdate<T>, IAdd<T>, IDelete where T : IVersionedEntity
    {
    }
}
