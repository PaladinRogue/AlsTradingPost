using Common.Domain.Concurrency.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
{
    public class VersionedDdto : IVersionedDdto
    {
        public IConcurrencyVersion Version { get; set; }
    }
}
