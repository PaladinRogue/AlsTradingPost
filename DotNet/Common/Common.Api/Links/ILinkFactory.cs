using System.Collections.Generic;
using Common.Api.Resources;

namespace Common.Api.Links
{
    public interface ILinkFactory
    {
        ILink Create(string linkName, string routeName, IEnumerable<AuthorisationLink> verbAuthorisationContextTypePairs, IResource resource, ITemplate template, string basePath = null);
    }
}
