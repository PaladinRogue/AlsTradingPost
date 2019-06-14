using System.Collections.Generic;
using Common.Api.Resources;

namespace Common.Api.Links
{
    public interface ILinkFactory
    {
        ILink Create<TResource, TTemplate>(string linkName, string routeName, IEnumerable<AuthorisationLink> verbAuthorisationContextTypePairs, TResource resource, TTemplate template, string basePath = null);
    }
}
