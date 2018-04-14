using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Interfaces
{
    public interface IVersionedDdto : IVersion<IConcurrencyVersion>
    {
    }
}
