using System.Collections.Generic;

namespace Common.Api.Links
{
    public class Links
    {
        public ILink Self { get; set; }

        public PagingLinks PagingLinks { get; set; }

        public IEnumerable<ILink> RelatedLinks { get; set; }
    }
}
