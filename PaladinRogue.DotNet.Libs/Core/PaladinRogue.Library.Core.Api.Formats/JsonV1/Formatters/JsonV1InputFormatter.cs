using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PaladinRogue.Library.Core.Api.Exceptions;
using PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Application;
using PaladinRogue.Library.Core.Application.Exceptions;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formatters
{
    public class JsonV1InputFormatter : JsonInputFormatter
    {
        private readonly MvcOptions _options;

        private readonly IArrayPool<char> _charPool;

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonV1InputFormatter(ILogger logger,
            JsonSerializerSettings serializerSettings,
            ArrayPool<char> charPool,
            ObjectPoolProvider objectPoolProvider,
            MvcOptions options,
            MvcJsonOptions jsonOptions)
            : base(logger, serializerSettings, charPool, objectPoolProvider, options, jsonOptions)
        {
            _options = options;
            _jsonSerializerSettings = serializerSettings;
            _charPool = new JsonArrayPool<char>(charPool);

            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(MediaTypes.JsonV1));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(MediaTypes.Problem));
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

                    switch (model?.Data)
                    {
                        case null when context.TreatEmptyInputAsDefaultValue:
                            return await InputFormatterResult.SuccessAsync(model);
                        case null:
                            return await InputFormatterResult.NoValueAsync();
                    }

                    string expectedType = modelType.GetCustomAttributes<ResourceTypeAttribute>().FirstOrDefault()?.Type;

                    if (!string.IsNullOrWhiteSpace(model.Data.Type) && model.Data.Type.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
                    {
                        return await InputFormatterResult.SuccessAsync(model.Data.Attributes.ToObject(modelType));
                    }

                    BusinessApplicationException exception = new BusinessApplicationException(ExceptionType.BadRequest, ErrorCodes.ResourceType, "The provided resource type is invalid");
                    ApplicationError applicationError = new ApplicationError
                    {
                        Exception = exception,
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };
                    string response = JsonConvert.SerializeObject(FormattedError.Create(applicationError.FormatError()), _jsonSerializerSettings);

                    context.HttpContext.Response.Clear();
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    context.HttpContext.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();

                    await context.HttpContext.Response.WriteAsync(response);

                    throw exception;
                }
            }
        }
    }
}
