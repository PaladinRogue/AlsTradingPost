using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency.Interfaces;
using Newtonsoft.Json;

namespace Common.Resources.Concurrency
{
    public static class ConcurrencyVersionFactory
    {
        public static IConcurrencyVersion CreateFromBase64String(string entityTagValue)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(entityTagValue);
            string thing = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject<ConcurrencyVersion>(thing);
        }

        public static IConcurrencyVersion CreateFromEntity(IEntity entity)
        {
            return new ConcurrencyVersion
            {
                Version = entity.Version
            };
        }
    }
}
