using Common.Domain.Concurrency.Interfaces;

namespace Common.Domain.Services.Domain
{
    public interface IUpdateService<in TIn, out TOut>
        where TIn : IVersionedDdto
        where TOut : IVersionedProjection
    {
        TOut Update(TIn entity);
    }
}
