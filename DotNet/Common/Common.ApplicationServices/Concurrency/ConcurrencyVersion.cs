using Common.Domain.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public class ConcurrencyVersion : IConcurrencyVersion
    {
        public byte[] Version { get; set; }
    }
}
