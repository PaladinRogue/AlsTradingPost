using System.Collections.Generic;

namespace Common.Api.Builders
{
    public class PropertyMeta
    {
        public string Name { get; set; }

        public IList<Constraint> Constraints { get; set; }
    }
}