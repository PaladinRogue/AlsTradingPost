
using Common.Resources.Concurrency.Interfaces;

namespace Common.Resources.Concurrency
{
    public class ConcurrencyVersion : IConcurrencyVersion
    {
        public byte[] Version { get; set; }
    }
}
