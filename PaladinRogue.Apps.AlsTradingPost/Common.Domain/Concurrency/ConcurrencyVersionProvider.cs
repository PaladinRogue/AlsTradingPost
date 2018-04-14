using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
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
