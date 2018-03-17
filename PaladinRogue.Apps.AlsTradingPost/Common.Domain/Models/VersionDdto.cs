using Common.Domain.Interfaces;

namespace Common.Domain.Models
{
    public class VersionedDdto : IVersionedDdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
