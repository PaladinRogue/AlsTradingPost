using System.Collections.Generic;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public interface IDynamicLinksProvider
    {
        IEnumerable<ILink> GetLinks();
    }
}
