using Common.Domain.Interfaces;

namespace Common.Domain.Models
{
    public class VersionedProjection : IVersionedProjection
    {
        public int Version { get; set; }
    }
}
