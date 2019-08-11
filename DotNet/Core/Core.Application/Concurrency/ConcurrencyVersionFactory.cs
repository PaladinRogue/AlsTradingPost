using System;
using Newtonsoft.Json;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Core.Application.Concurrency
{
    public static class ConcurrencyVersionFactory
    {
        public static IConcurrencyVersion CreateFromBase64String(string entityTagValue)
        {
            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(entityTagValue);
                string stringConcurrencyVersion = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                return JsonConvert.DeserializeObject<ConcurrencyVersion>(stringConcurrencyVersion);
            }
            catch (Exception)
            {
                throw new BusinessApplicationException(ExceptionType.Concurrency, "Could not decode concurrency token");
            }
        }

        public static IConcurrencyVersion CreateFromEntity(IVersionedEntity entity)
        {
            return new ConcurrencyVersion
            {
                Version = entity.Version
            };
        }
    }
}
