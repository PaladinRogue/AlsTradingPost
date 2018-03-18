using Common.Domain.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Domain.Models
{
    public class VersionedProjection : IVersionedProjection
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
