using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Concurrency
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion Get();
    }
}