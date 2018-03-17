using System;
using Common.Domain.Interfaces;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Providers
{
    public class ConcurrencyVersionProvider : IConcurrencyVersionProvider
    {
        public IConcurrencyVersion GetConcurrencyVersion(IEntity entity)
        {
            return new ConcurrencyVersion(entity);
        }

        public byte[] GetConcurrencyTimeStamp(IVersionedDdto entityDdto)
        {
            return entityDdto.Version.Version;
        }
    }

}
