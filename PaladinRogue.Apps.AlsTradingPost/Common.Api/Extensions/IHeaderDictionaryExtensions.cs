using Microsoft.AspNetCore.Http;

namespace Common.Api.Extensions
{
    public static class IHeaderDictionaryExtensions
    {
        public static void ETag(this IHeaderDictionary headerDictionary)
        {
//            headerDictionary["ETag"];
        }
    }
}
