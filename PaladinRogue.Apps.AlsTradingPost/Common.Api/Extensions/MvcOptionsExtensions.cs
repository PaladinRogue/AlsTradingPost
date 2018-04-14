using System.Buffers;
using Common.Api.Authentication;
using Common.Api.Concurrency;
using Common.Api.ResourceFormatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Api.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseCustomJsonOutputFormatter(this MvcOptions options)
        {
            // Remove any json output formatter 
            options.OutputFormatters.RemoveType<JsonOutputFormatter>();

            // Add custom json output formatter 
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            options.OutputFormatters.Add(new CustomJsonOutputFormatter(jsonSerializerSettings, ArrayPool<char>.Shared));

            return options;
        }

        public static MvcOptions UseConcurrencyFilter(this MvcOptions options)
        {
            options.Filters.Add(new ConcurrencyActionFilter());

            return options;
        }

        public static MvcOptions UseAppAccessAuthorizeFilter(this MvcOptions options)
        {
            options.Conventions.Add(new AuthorizeAppAccessControllerFilter());

            return options;
        }

        public static MvcOptions RequireHttps(this MvcOptions options)
        {
            options.Filters.Add(new RequireHttpsAttribute());

            return options;
        }
    }
}
