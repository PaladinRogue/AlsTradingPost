using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Concurrency
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion Get();
    }
}