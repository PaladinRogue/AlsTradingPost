using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using PaladinRogue.Libray.Core.Api.Formats.JsonV1.Formatters;

namespace PaladinRogue.Libray.Core.Api.Formats.JsonV1
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions UseJsonV1InputFormatter(this MvcOptions options,
            ILogger<MvcOptions> logger,
            ObjectPoolProvider objectPoolProvider,
            MvcJsonOptions jsonOptions,
            JsonSerializerSettings serializerSettings)
        {
            options.InputFormatters.RemoveType<JsonInputFormatter>();

            JsonV1InputFormatter jsonV1InputFormatter = new JsonV1InputFormatter(logger,
                serializerSettings,
                ArrayPool<char>.Shared,
                objectPoolProvider,
                options,
                jsonOptions);

            options.InputFormatters.Add(jsonV1InputFormatter);

            return options;
        }

        public static MvcOptions UseJsonV1OutputFormatter(this MvcOptions options, JsonSerializerSettings serializerSettings)
        {
            options.OutputFormatters.RemoveType<JsonOutputFormatter>();

            JsonV1OutputFormatter jsonV1OutputFormatter = new JsonV1OutputFormatter(serializerSettings, ArrayPool<char>.Shared);

            options.OutputFormatters.Add(jsonV1OutputFormatter);

            return options;
        }
    }
}