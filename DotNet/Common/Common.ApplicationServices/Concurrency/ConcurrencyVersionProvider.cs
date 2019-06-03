using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public class ConcurrencyVersionProvider : IConcurrencyVersionProvider
    {
        public IConcurrencyVersion GetConcurrencyVersion(IVersionedEntity entity)
        {
            return ConcurrencyVersionFactory.CreateFromEntity(entity);
        }

        public byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto)
        {
            return entityDdto.Version.Version;
        }
    }

}
