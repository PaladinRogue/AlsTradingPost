using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.ResourceFormatter.Resources;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace Common.Api.ResourceFormatter
{
    public class CustomJsonOutputFormatter : JsonOutputFormatter
    {
        public CustomJsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool)
            : base(serializerSettings, charPool)
        {
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            if (context.Object == null)
            {
                await base.WriteResponseBodyAsync(context, selectedEncoding);
            }

            //TODO nothing returns ieneumerable anymore
            if (context.ObjectType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                await WriteResponse(context, selectedEncoding, new CollectionResource(context));
            }
            else
            {
                await WriteResponse(context, selectedEncoding, new Resource(context.Object));
            }
        }

        private async Task WriteResponse(OutputFormatterWriteContext context, Encoding selectedEncoding, object response)
        {
            using (TextWriter writer = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
                WriteObject(writer, response);

                await writer.FlushAsync();
            }
        }
    }
}
