using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Api.Formats.JsonV1.Formats;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formatters
{
    public class JsonV1InputFormatter : JsonInputFormatter
    {
        private const string JsonV1MediaType = "application/vnd.api+json";
        private const string ProblemMediaType = "application/problem+json";

        private readonly MvcOptions _options;

        private readonly IArrayPool<char> _charPool;

        public JsonV1InputFormatter(ILogger logger,
            JsonSerializerSettings serializerSettings,
            ArrayPool<char> charPool,
            ObjectPoolProvider objectPoolProvider,
            MvcOptions options,
            MvcJsonOptions jsonOptions)
            : base(logger, serializerSettings, charPool, objectPoolProvider, options, jsonOptions)
        {
            _options = options;
            _charPool = new JsonArrayPool<char>(charPool);

            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(JsonV1MediaType));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ProblemMediaType));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            HttpRequest request = context.HttpContext.Request;
            bool flag = _options.SuppressInputFormatterBuffering;

            if (!request.Body.CanSeek && !flag)
            {
                request.EnableBuffering();
                await request.Body.DrainAsync(CancellationToken.None);
                request.Body.Seek(0L, SeekOrigin.Begin);
            }
            using (TextReader reader = context.ReaderFactory(request.Body, encoding))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
                {
                    jsonTextReader.ArrayPool = _charPool;
                    jsonTextReader.CloseInput = false;
                    Type modelType = context.ModelType;
                    JsonSerializer jsonSerializer = CreateJsonSerializer();
                    Request model;
                    try
                    {
                        model = jsonSerializer.Deserialize<Request>(jsonTextReader);
                    }
                    finally
                    {
                        ReleaseJsonSerializer(jsonSerializer);
                    }

                    if (model?.Data == null && context.TreatEmptyInputAsDefaultValue)
                    {
                        InputFormatterResult.Success(model);
                    }

                    if (model?.Data == null)
                    {
                        return InputFormatterResult.NoValue();
                    }

                    string expectedType = modelType.GetCustomAttributes<ResourceTypeAttribute>().FirstOrDefault()?.Type;

                    if (string.IsNullOrWhiteSpace(model.Data.Type) || model.Data.Type != expectedType)
                    {
                        throw new BadRequestException();
                    }

                    return InputFormatterResult.Success(model.Data.Attributes.ToObject(modelType));
                }
            }
        }
    }
}
