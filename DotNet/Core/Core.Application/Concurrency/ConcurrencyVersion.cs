using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Application.Concurrency
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
