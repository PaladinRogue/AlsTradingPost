using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Application.Concurrency
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
