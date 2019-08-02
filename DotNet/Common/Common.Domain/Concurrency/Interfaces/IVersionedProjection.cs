using Common.Domain.Models;

namespace Common.Domain.Concurrency.Interfaces
{
    public interface IVersionedProjection : IProjection, IVersion<IConcurrencyVersion>
    {
    }
}
