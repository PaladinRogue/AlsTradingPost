using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AlsTradingPost.Api.Responses
{
    public class CollectionMeta
    {
        public CollectionMeta(OutputFormatterWriteContext context)
        {
            if (context.Object is IEnumerable<dynamic> objects)
            {
                Count = objects.Count();
            }
        }

        //TODO add paging info etc from the context
        //TODO how to create links on collection metadata?

        public int Count { get; set; }
    }
}