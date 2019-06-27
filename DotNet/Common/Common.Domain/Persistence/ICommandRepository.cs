using Common.Domain.Aggregates;

namespace Common.Domain.Persistence
{
    public interface ICommandRepository<T> : IUpdateCommand<T>, IAddCommand<T>, IDelete, IGetByIdQuery<T>, IGetSingleQuery<T>, ICheckConcurrencyQuery<T> where T : IAggregateRoot
    {
    }
}
