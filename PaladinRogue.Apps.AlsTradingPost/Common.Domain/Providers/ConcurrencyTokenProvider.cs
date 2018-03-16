using System;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Providers
{
    public class ConcurrencyTokenProvider : IConcurrencyTokenProvider
    {

        public int GetConcurrencyToken(IEntity entity)
        {
            return entity.GetConcurrencyVersion();
        }

        public byte[] GetConcurrencyToken(IVersionedDdto entity)
        {
            var bytes = BitConverter.GetBytes(entity.Version);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
    }

}
