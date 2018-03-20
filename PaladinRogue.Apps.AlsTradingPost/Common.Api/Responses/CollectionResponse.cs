using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Common.Api.Responses
{
    public class CollectionResponse
    {
        public CollectionResponse(OutputFormatterWriteContext context)
        {
            if (!(context.Object is IEnumerable<dynamic>)) return;

            CollectionData = new List<Response>();

            foreach (dynamic data in context.Object as IEnumerable<dynamic>)
            {
                CollectionData.Add(new Response(data));
            }

            CollectionMeta = new CollectionMeta(context);
        }

        public IList<Response> CollectionData { get; set; }
        public CollectionMeta CollectionMeta { get; set; }
    }
}