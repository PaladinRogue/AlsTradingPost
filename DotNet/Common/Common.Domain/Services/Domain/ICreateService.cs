using Common.Domain.Concurrency.Interfaces;

namespace Common.Domain.Services.Domain
{
    public interface ICreateService<in TIn, out TOut>
        where TOut : IVersionedProjection
    {
        TOut Create(TIn entity);
    }
}
