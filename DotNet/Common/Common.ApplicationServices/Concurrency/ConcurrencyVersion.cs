using Common.Domain.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public class ConcurrencyVersion : IConcurrencyVersion
    {
        public int Version { get; set; }

        public override string ToString()
        {
            return Version.ToString();
        }
    }
}
