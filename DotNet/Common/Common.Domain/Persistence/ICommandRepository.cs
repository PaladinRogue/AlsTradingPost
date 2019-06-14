using Common.Domain.Models.Interfaces;

namespace Common.Domain.Persistence
{
    public interface ICommandRepository<T> : IUpdateCommand<T>, IAddCommand<T>, IDelete, IGetByIdQuery<T>, IGetSingleQuery<T> where T : IAggregateRoot
    {
    }
}
