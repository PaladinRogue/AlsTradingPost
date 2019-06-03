using Common.Domain.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
{
    public class VersionedProjection : IVersionedProjection
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
