using System.Collections.Generic;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public interface ILink
    {
        string Name { get; set; }

        string Uri { get; set; }

        IDictionary<string, object> QueryParams { get; set; }

        HttpVerb AllowVerbs { get; set; }
    }
}