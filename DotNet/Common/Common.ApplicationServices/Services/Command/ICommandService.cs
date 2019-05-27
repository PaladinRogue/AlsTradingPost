using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Services.Command
{
    public interface ICommandService<in T> : IUpdateCommandService<T>, ICreateCommandService<T> where T : IAggregateRoot
    {
    }
}
