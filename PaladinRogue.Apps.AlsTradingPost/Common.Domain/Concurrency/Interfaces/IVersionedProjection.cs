using Common.Domain.Services;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Interfaces
{
    public interface IVersionedProjection : IProjection, IVersion<IConcurrencyVersion>
    {
    }
}
