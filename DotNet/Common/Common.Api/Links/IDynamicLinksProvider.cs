using System.Collections.Generic;

namespace Common.Api.Links
{
    public interface IDynamicLinksProvider
    {
        IEnumerable<ILink> GetLinks();
    }
}
