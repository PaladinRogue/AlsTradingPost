using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency.Interfaces
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion GetConcurrencyVersion(IVersionedEntity entity);
        byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto);
    }
}
