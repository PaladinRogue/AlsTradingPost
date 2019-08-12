using System.Collections.Generic;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Library.Core.Api.Links
{
    public interface ILink
    {
        string Name { get; set; }

        string Uri { get; set; }

        IDictionary<string, object> QueryParams { get; set; }

        HttpVerb AllowVerbs { get; set; }
    }
}