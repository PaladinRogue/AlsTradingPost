using Common.Domain.Models.Interfaces;

namespace Common.Domain.Services.Command
{
    public interface ICommandService<T> : IUpdateCommandService<T>, ICreateCommandService<T> where T : IEntity
    {
    }
}
