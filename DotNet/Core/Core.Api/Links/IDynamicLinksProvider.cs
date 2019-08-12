using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.Links
{
    public interface IDynamicLinksProvider
    {
        IEnumerable<ILink> GetLinks();
    }
}
