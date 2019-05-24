using Common.Domain.Models;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Interfaces
{
    public interface IVersionedProjection : IProjection, IVersion<IConcurrencyVersion>
    {
    }
}
