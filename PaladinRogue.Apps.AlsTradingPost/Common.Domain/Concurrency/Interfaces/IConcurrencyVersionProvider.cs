using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Interfaces
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion GetConcurrencyVersion(IEntity entity);
        byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto);
    }
}
