using System;
using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Constants
{
    public class HttpVerbMapper
    {
        private static readonly IReadOnlyDictionary<string, HttpVerb> HttpVerbMap = new Dictionary<string, HttpVerb>
        {
            ["DELETE"] = HttpVerb.Delete,
            ["GET"] = HttpVerb.Get,
            ["POST"] = HttpVerb.Post,
            ["PUT"] = HttpVerb.Put,
        };

        public static HttpVerb GetVerb(string httpVerb)
        {
            if (HttpVerbMap.ContainsKey(httpVerb))
            {
                return HttpVerbMap[httpVerb];
            }

            throw new ArgumentException($"HttpVerb mapping is not supported for { httpVerb }");
        }

    }
}
