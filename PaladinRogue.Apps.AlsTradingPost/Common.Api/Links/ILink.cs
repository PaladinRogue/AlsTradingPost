using System.Collections.Generic;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    public interface ILink
    {
        string Name { get; set; }

        string Uri { get; set; }

        IDictionary<string, object> QueryParams { get; set; }

        HttpVerb AllowVerbs { get; set; }
    }
}