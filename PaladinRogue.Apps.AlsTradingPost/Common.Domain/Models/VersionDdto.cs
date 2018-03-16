using Common.Domain.Interfaces;

namespace Common.Domain.Models
{
    public class VersionedDdto : IVersionedProjection
    {
        public int Version { get; set; }
    }
}
