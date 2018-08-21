using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Api.Formats.JsonV1
{
    public class ConfigureJsonV1MvcOptions : IConfigureOptions<MvcOptions>
    {
        private readonly ILogger<MvcOptions> _logger;
        private readonly ObjectPoolProvider _objectPoolProvider;
        private readonly MvcJsonOptions _jsonOptions;

        public ConfigureJsonV1MvcOptions(ILogger<MvcOptions> logger,
            ObjectPoolProvider objectPoolProvider,
            IOptions<MvcJsonOptions> jsonOptions)
        {
            _logger = logger;
            _objectPoolProvider = objectPoolProvider;
            _jsonOptions = jsonOptions.Value;
        }

        public void Configure(MvcOptions options)
        {
            _jsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            options.UseJsonV1InputFormatter(_logger, _objectPoolProvider, _jsonOptions, _jsonOptions.SerializerSettings)
                .UseJsonV1OutputFormatter(_jsonOptions.SerializerSettings);
        }
    }
}
