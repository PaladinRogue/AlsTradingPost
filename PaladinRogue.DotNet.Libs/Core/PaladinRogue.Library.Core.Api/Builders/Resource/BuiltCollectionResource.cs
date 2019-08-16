using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.Builders.Resource
{
    public class BuiltCollectionResource
    {
        public int? TotalResults { get; set; }

        public IEnumerable<BuiltResource> BuiltResources { get; set; }

        public Links.Links Links { get; set; }
    }
}
