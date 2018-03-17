using Common.Domain.Interfaces;

namespace Common.Domain.Models
{
    public class VersionedProjection : IVersionedProjection
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
