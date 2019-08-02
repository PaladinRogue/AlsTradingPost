using Common.Domain.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
{
    public class VersionedDdto : IVersionedDdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
