using Common.Domain.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
{
    public class VersionedProjection : IVersionedProjection
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
