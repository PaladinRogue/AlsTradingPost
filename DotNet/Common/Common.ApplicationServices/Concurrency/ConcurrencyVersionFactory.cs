﻿using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
using Newtonsoft.Json;

namespace Common.ApplicationServices.Concurrency
{
    public static class ConcurrencyVersionFactory
    {
        public static IConcurrencyVersion CreateFromBase64String(string entityTagValue)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(entityTagValue);
            string stringConcurrencyVersion = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject<ConcurrencyVersion>(stringConcurrencyVersion);
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