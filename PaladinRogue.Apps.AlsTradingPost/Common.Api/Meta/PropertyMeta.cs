using System.Collections.Generic;
using Common.Api.Builders;

namespace Common.Api.Meta
{
    public class PropertyMeta
    {
        public string Name { get; set; }

        public IList<Constraint> Constraints { get; set; }
    }
}