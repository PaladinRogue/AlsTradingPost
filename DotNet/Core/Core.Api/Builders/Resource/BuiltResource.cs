using System;
using System.Collections.Generic;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Libray.Core.Api.Builders.Resource
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
