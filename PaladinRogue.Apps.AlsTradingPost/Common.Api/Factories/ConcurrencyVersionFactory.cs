using Common.Api.Interfaces;
using Common.Api.Request;
using Newtonsoft.Json;

namespace Common.Api.Factories
{
    public static class ConcurrencyVersionFactory
    {
        public static IConcurrencyVersion Create(string entityTagValue)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(entityTagValue);
            var thing = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return JsonConvert.DeserializeObject<ConcurrencyVersion>(thing);
        }
    }
}
