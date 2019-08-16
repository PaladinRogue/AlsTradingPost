using PaladinRogue.Library.Core.Domain.Aggregates;

namespace PaladinRogue.Library.Core.Domain.Persistence
{
    public interface ICommandRepository<T> : IUpdateCommand<T>, IAddCommand<T>, IDeleteCommand, IGetQuery<T>, IGetByIdQuery<T>, IGetSingleQuery<T>, ICheckConcurrencyQuery<T> where T : IAggregateRoot
    {
    }
}
