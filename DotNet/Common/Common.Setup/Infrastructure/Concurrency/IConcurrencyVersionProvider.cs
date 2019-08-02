using Common.Domain.Concurrency.Interfaces;

namespace Common.Setup.Infrastructure.Concurrency
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion Get();
    }
}