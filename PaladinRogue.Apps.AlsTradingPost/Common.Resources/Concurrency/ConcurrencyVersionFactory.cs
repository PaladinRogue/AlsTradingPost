using Common.Domain.Models.Interfaces;
using Newtonsoft.Json;

namespace Common.Resources.Concurrency
{
    public static class ConcurrencyVersionFactory
    {
        public static IConcurrencyVersion CreateFromBase64String(string entityTagValue)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(entityTagValue);
            var thing = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
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
