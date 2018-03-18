using Common.Resources.Concurrency;

namespace Common.Domain.Interfaces
{
    public interface IVersionedProjection : IProjection, IVersion<IConcurrencyVersion>
    {
    }
}
