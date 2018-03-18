using Common.Domain.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Domain.Models
{
    public class VersionedDdto : IVersionedDdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
