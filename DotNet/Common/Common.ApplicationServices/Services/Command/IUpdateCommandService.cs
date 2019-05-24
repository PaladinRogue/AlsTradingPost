using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Services.Command
{
    public interface IUpdateCommandService<in T> where T : IEntity
    {
        void Update(T entity);
    }
}
