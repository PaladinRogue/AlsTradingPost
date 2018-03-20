using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Domain.Providers
{
    public class ConcurrencyVersionProvider : IConcurrencyVersionProvider
    {
        public IConcurrencyVersion GetConcurrencyVersion(IEntity entity)
        {
            return ConcurrencyVersionFactory.CreateFromEntity(entity);
        }

        public byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto)
        {
            return entityDdto.Version.Version;
        }
    }

}
