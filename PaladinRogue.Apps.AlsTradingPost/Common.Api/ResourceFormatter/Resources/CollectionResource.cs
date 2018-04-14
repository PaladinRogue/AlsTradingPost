using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Common.Api.ResourceFormatter.Resources
{
    public class CollectionResource
    {
        public CollectionResource(OutputFormatterWriteContext context)
        {
            if (!(context.Object is IEnumerable<dynamic>)) return;

            CollectionData = new List<Resource>();

            foreach (dynamic data in context.Object as IEnumerable<dynamic>)
            {
                CollectionData.Add(new Resource(data));
            }

            CollectionMeta = new CollectionMeta(context);
        }

        public IList<Resource> CollectionData { get; set; }
        public CollectionMeta CollectionMeta { get; set; }
    }
}