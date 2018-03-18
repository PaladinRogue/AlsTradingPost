using Common.Resources.Concurrency;

namespace Common.Domain.Interfaces
{
    public interface IVersionedDdto : IVersion<IConcurrencyVersion>
    {
    }
}
