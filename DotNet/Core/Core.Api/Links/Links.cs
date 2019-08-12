using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.Links
{
    public class Links
    {
        public ILink Self { get; set; }

        public PagingLinks PagingLinks { get; set; }

        public IEnumerable<ILink> RelatedLinks { get; set; }
    }
}
