using System;
using System.Collections.Generic;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Api.Builders.Resource
{
    public class BuiltResource
    {
        public Guid? Id { get; set; }

        public Type Type { get; set; }

        public IEnumerable<Property> Properties { get; set; }

        public Links.Links Links { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}
