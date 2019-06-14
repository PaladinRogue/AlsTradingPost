using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Builders.Resource;
using Common.Api.Exceptions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Common.Api.Formats.JsonV1.Formatters
{
    public class JsonV1OutputFormatter : JsonOutputFormatter
    {
        private const string JsonV1MediaType = "application/vnd.api+json";

        public JsonV1OutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool)
            : base(serializerSettings, charPool)
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(JsonV1MediaType));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (selectedEncoding == null)
            {
                throw new ArgumentNullException(nameof(selectedEncoding));
            }

            TextWriter writer = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding);
            try
            {
                switch (context.Object)
                {
                    case BuiltResource builtResource:
                        WriteObject(writer, ResponseFactory.Create(builtResource, context.HttpContext.Request));
                        break;
                    case BuiltCollectionResource builtCollectionResource:
                        WriteObject(writer, ResponseFactory.Create(builtCollectionResource, context.HttpContext.Request));
                        break;
                    case IFormattedError formattedError:
                        WriteObject(writer, formattedError);
                        break;
                }

                await writer.FlushAsync();
            }
            finally
            {
                writer?.Dispose();
            }
        }
    }
}
