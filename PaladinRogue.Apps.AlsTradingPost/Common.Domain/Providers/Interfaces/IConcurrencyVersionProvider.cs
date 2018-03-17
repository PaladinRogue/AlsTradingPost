using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Providers.Interfaces
{
    public interface IConcurrencyVersionProvider
    {
        IConcurrencyVersion GetConcurrencyVersion(IEntity entity);
        byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto);
    }
}
