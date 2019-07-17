using Common.Domain.Aggregates;

namespace Common.Domain.Persistence
{
    public interface ICommandRepository<T> : IUpdateCommand<T>, IAddCommand<T>, IDeleteCommand, IGetByIdQuery<T>, IGetSingleQuery<T>, ICheckConcurrencyQuery<T> where T : IAggregateRoot
    {
    }
}
