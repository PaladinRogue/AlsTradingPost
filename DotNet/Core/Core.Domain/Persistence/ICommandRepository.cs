using PaladinRogue.Libray.Core.Domain.Aggregates;

namespace PaladinRogue.Libray.Core.Domain.Persistence
{
    public interface ICommandRepository<T> : IUpdateCommand<T>, IAddCommand<T>, IDeleteCommand, IGetQuery<T>, IGetByIdQuery<T>, IGetSingleQuery<T>, ICheckConcurrencyQuery<T> where T : IAggregateRoot
    {
    }
}
